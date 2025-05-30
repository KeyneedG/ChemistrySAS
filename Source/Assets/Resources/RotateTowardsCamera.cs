using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{
    private Transform cam;
    private static Transform fakeCam;

    private void Awake()
    {
        cam = Camera.main.transform;
        if (fakeCam == null)
        {
            fakeCam = new GameObject("fakeCam").transform;
        }
    }

    private void Update()
    {
        fakeCam.position = Vector3.Lerp(fakeCam.position, cam.position, Time.deltaTime * 5f);
        transform.LookAt(fakeCam.position);
    }
}
