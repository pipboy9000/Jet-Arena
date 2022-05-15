using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public GameObject explosion;

    Rigidbody rgb;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per fram
    void Update()
    {
        //transform.position += transform.forward * Time.deltaTime * 300;
    }

    private void FixedUpdate()
    {
        rgb.AddForce(transform.forward * Time.deltaTime * 900, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(explosion, collision.contacts[0].point, transform.rotation);
    }
}
