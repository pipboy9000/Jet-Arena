using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Rigidbody rgb;

    public Transform ship;
    ShipController shipController;


    // Start is called before the first frame update
    void Start()
    {
        shipController = ship.GetComponent<ShipController>();
        rgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 pushVec = (shipController.tracPoint.position - transform.position);
        float distance = Vector3.Distance(shipController.tracPoint.position, transform.position);
        float dotProd = 1 - Vector3.Dot(pushVec.normalized, shipController.transform.forward);

        Debug.Log("distance: " + distance + "   dot: " + dotProd);

        if (shipController.tractorOn)
        {
            rgb.AddForce(pushVec);
        }
    }
}
