using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float rotateSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateUsingAD();
    }

    //If want to change rotation control
    void RotateUsingQE()
    {
        bool rotateRight = Input.GetKey(KeyCode.E);
        bool rotateLeft = Input.GetKey(KeyCode.Q);

        if (rotateRight == true)
        {
            transform.Rotate(Vector3.up, -1 * rotateSpeed * Time.deltaTime);
        }
        else if (rotateLeft == true)
        {
            transform.Rotate(Vector3.up, 1 * rotateSpeed * Time.deltaTime);
        }
    }

    void RotateUsingAD()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, -horizontalInput * rotateSpeed * Time.deltaTime);
    }
}
