using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per fram
    void Update()
    {
        //transform.position += transform.forward * Time.deltaTime * 300;
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * 300;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Destroy(gameObject);
    //    Instantiate(explosion, transform.position, transform.rotation);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        Destroy(gameObject);
        Instantiate(explosion, collision.contacts[0].point, transform.rotation);
    }
}
