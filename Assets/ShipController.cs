using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShipController : MonoBehaviour
{

    Transform aim;
    Transform lookAt;
    public Vector3 lookTarget;
    public Vector3 hitTarget;
    public bool hit;
    Rigidbody m_Rigidbody;

    Transform rightWing;
    Transform leftWing;
    Transform topRightWing;
    Transform topLeftWing;
    Transform leftCannon;
    Transform rightCannon;
    Transform currentCannonFire;
    public float fireRecoilDelay = 0.1f;
    float currentRecoilDelay = 0;

    ParticleSystem engineParticles;

    float vertical;
    float strafe;
    float mouseDx;
    float mouseDy;

    public float turnSpeed = 5;
    public float lookSpeed = 150;
    public float spinSpeed = 5;
    public float maxSpeed = 30;
    public float minSpeed = 10;

    float currentSpeed = 0;
    float lastSpeed = 0;
    float acc;
    Vector3 lastPosition;

    Transform burner;

    public Transform Laser;

    Renderer burnerRenderer;
    Material material;

    VisualEffect leftCannonFlash;
    VisualEffect rightCannonFlash;

    Light rightCannonLight;
    Light leftCannonLight;

    // Start is called before the first frame update
    void Start()
    {
        aim = transform.Find("Aim");
        lookAt = transform.Find("Look At");
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.angularDrag = 5;
        m_Rigidbody.drag = 1;

        Transform ShipModel = transform.Find("Ship Model");
        leftWing = ShipModel.Find("Left Wing");
        rightWing = ShipModel.Find("Right Wing");
        topRightWing = ShipModel.Find("Top Right Wing");
        topLeftWing = ShipModel.Find("Top Left Wing");
        burner = transform.Find("Burner");
        engineParticles = transform.Find("Engine Particles").GetComponent<ParticleSystem>();
        burnerRenderer = burner.GetComponent<Renderer>();
        material = burnerRenderer.material;
        leftCannon = transform.Find("Left Cannon");
        rightCannon = transform.Find("Right Cannon");
        currentCannonFire = rightCannon;

        leftCannonFlash = transform.Find("Left Cannon Flash").GetComponent<VisualEffect>();
        rightCannonFlash = transform.Find("Right Cannon Flash").GetComponent<VisualEffect>();

        rightCannonLight = transform.Find("Right Cannon Light").GetComponent<Light>();
        leftCannonLight = transform.Find("Left Cannon Light").GetComponent<Light>();
    }

    void Fire()
    {
        Instantiate(Laser, currentCannonFire.position, aim.rotation);
        currentCannonFire = currentCannonFire == leftCannon ? rightCannon : leftCannon;
        currentRecoilDelay = fireRecoilDelay;
        if (currentCannonFire == rightCannon)
        {
            leftCannonFlash.Play();
            leftCannonLight.intensity = 5;
        }
        else
        {
            rightCannonFlash.Play();
            rightCannonLight.intensity = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot
        if (Input.GetMouseButton(0))
        {
            if(currentRecoilDelay <=0)
            {
                Fire();
            }
        }

        currentRecoilDelay -= Time.deltaTime;
        rightCannonLight.intensity *= 0.85f;
        leftCannonLight.intensity *= 0.85f;

        //Inputs

        vertical = Input.GetAxis("Vertical");

        strafe = Input.GetAxis("Horizontal");

        mouseDx = Input.GetAxis("Mouse X");

        mouseDy = Input.GetAxis("Mouse Y");

        //wings

        leftWing.localRotation = Quaternion.Lerp(leftWing.localRotation, Quaternion.Euler(strafe * 30 + 30, 0, 0), .85f);

        rightWing.localRotation = Quaternion.Lerp(rightWing.localRotation, Quaternion.Euler(-strafe * 30 + 30, 0, 0), .85f);

        //top wings

        float rightTurnAmount = Mathf.Max(0, -strafe);
        float leftTurnAmount = Mathf.Max(0, strafe);
        float angularSpeed = Mathf.Min(1.5f, m_Rigidbody.angularVelocity.magnitude);

        float jitter = (acc / 5 + angularSpeed / 10) * (currentSpeed / 10) / 30;

        //Debug.Log("angularSpeed: " + angularSpeed + "    jitter: " + jitter + "  acc: " + acc + "  speed: " + currentSpeed);

        topRightWing.localEulerAngles =
            new Vector3(Mathf.PerlinNoise(Time.time * jitter, 0f) * (rightTurnAmount + 0.1f + acc + angularSpeed) * -30,
            topRightWing.localEulerAngles.y,
            topRightWing.localEulerAngles.z);

        topLeftWing.localEulerAngles =
            new Vector3(Mathf.PerlinNoise(Time.time * jitter, 0.3f) * (leftTurnAmount + 0.1f + acc + angularSpeed) * -30,
            topLeftWing.localEulerAngles.y,
            topLeftWing.localEulerAngles.z);

        //burner
        material.color = new Color(0, 1, 1) * (acc * 50 + 0.1f) * 5 * Mathf.PerlinNoise(Time.time * 50, 0);
        burner.localScale = new Vector3(burner.localScale.x, burner.localScale.y, 1.1f + acc * 5 + currentSpeed / 50);

        //Aim Rotation

        aim.Rotate(new Vector3(mouseDy, mouseDx, 0f) * Time.deltaTime * lookSpeed);

        aim.localRotation = Quaternion.Lerp(aim.localRotation, Quaternion.Euler(0f, 0f, 0f), 0.1f);

        lookTarget = Vector3.Lerp(lookTarget, aim.transform.position + aim.transform.forward * 500, .1f);

        lookAt.position = lookTarget;
    }

    private void FixedUpdate()
    {
        //calc speed

        currentSpeed = (((transform.position - lastPosition).magnitude) / Time.deltaTime);

        lastPosition = transform.position;

        acc = Mathf.Abs(currentSpeed - lastSpeed);

        acc = Mathf.Min(acc, 1.5f);

        lastSpeed = currentSpeed;

        //Ship Rotation

        m_Rigidbody.AddRelativeTorque(new Vector3(aim.localRotation.x * turnSpeed, aim.localRotation.y * turnSpeed / 2, -strafe * spinSpeed),ForceMode.Acceleration);

        m_Rigidbody.AddForce(transform.forward * Mathf.Max(minSpeed, vertical * maxSpeed));

        //Crosshair
        RaycastHit rayHit;

        if(Physics.Raycast(aim.transform.position, aim.transform.forward, out rayHit, 500)) {

            // Hit
            hit = true;
            hitTarget = rayHit.point;

        } else {

            //No Hit
            hit = false;
        }
    }
}
