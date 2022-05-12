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
        Vector3 pushVec = (shipController.tracPoint.position - transform.position);
        float distance = Vector3.Distance(shipController.tracPoint.position, transform.position);
        float dotProd = Vector3.Dot(pushVec.normalized, shipController.transform.forward);

        Debug.Log("distance: " + distance + "   dot: " + dotProd);

        if (
            shipController.tractorOn &&
            distance < 100 &&
            dotProd < -0.5
            )
        {
            rgb.AddForce(pushVec.normalized * (50 - distance) * (dotProd + 1), ForceMode.Force);
        }
    }

    private void FixedUpdate()
    {

    }
}
