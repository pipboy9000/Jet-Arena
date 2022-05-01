using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject follow;
    public GameObject lookAt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //look at lerp

        Vector3 direction = lookAt.transform.position - transform.position;
        
        Quaternion toRotation = Quaternion.LookRotation(direction, follow.transform.up);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);

        
        //cam position
        
        transform.position = Vector3.Lerp(transform.position, follow.transform.position, 0.25f);
        
    }
}
