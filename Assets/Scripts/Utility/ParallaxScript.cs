using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 1f;  // Controls the speed of the parallax effect

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        float deltaMovement = cameraTransform.position.x - lastCameraPosition.x;
        transform.position += Vector3.right * (deltaMovement * parallaxFactor);

        lastCameraPosition = cameraTransform.position;
    }
}
