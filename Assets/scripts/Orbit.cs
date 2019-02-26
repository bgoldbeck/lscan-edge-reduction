using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float camera_distance = 20f;
    public float ang_x = 0.0f;
    public float ang_y = 0.0f;
    
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        cube.transform.position = Vector3.zero;
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Input.GetAxis("Mouse X") != 0f)
            {
                Debug.Log("Moving x");
                ang_x += Input.GetAxis("Mouse X");
            }
            if (Input.GetAxis("Mouse Y") != 0f)
            {
                Debug.Log("Moving y");
                ang_y -= Input.GetAxis("Mouse Y");
            }
        }

        Quaternion rotation = Quaternion.Euler(
              ang_y,
              ang_x,
              0.0f);

        Vector3 v_rotated = rotation * (Vector3.back * camera_distance);

        Vector3 position = cube.transform.position + v_rotated;

        Debug.Log(transform.eulerAngles);
        transform.position = position;
        transform.rotation = rotation;
        
        return;
    }
}
