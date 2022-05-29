using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject follow;
    public GameObject lookAt;
    public Transform ball;
    public Transform ship;

    enum MODE {forward, ball};

    MODE camMode;

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camMode = camMode == MODE.ball ? MODE.forward : MODE.ball;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(camMode == MODE.forward)
        {
            //cam position

            transform.position = Vector3.Lerp(transform.position, follow.transform.position, 0.25f);

            //look at lerp

            Vector3 direction = lookAt.transform.position - transform.position;

            Quaternion toRotation = Quaternion.LookRotation(direction, follow.transform.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);

        } else if (camMode == MODE.ball)
        {
            //new cam pos
            Vector3 newPos = ship.position + (ball.position - ship.position).normalized * -20;

            transform.position = Vector3.Lerp(transform.position, newPos, 0.25f);

            //look at lerp

            Vector3 direction = ball.transform.position - transform.position;

            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);
        }
        
    }
}
