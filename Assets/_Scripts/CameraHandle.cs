using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    public GameObject camera;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = camera.transform.position;
        transform.rotation = camera.transform.rotation;
    }
}
