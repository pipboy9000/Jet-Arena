using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Rigidbody rgb;
    public float energy = 0;
    Material shader;

    public void Charge(float amount)
    {
        energy += amount;
        energy = Mathf.Clamp(energy, 0, 500);
    }

    public void Shoot(Vector3 dir)
    {
        rgb.AddForce(dir * energy * 20);
    }

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
        //shader = GetComponent<Renderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        //if(energy > 0)
        //{
        //    energy *= 0.93f;
        //}

        //shader.SetFloat("Glow", energy / 500);
    }

    private void FixedUpdate()
    {

    }
}
