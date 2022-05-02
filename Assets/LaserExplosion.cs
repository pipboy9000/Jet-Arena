using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserExplosion : MonoBehaviour
{

    Light light;

    // Start is called before the first frame update
    void Start()
    {
        light = transform.Find("Light").GetComponent<Light>();
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = light.intensity * 0.9f;
    }
}
