using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    bool mouseLock = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            mouseLock = !mouseLock;
        }

        Cursor.lockState = mouseLock ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
