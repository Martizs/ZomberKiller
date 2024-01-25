using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera fpsCamera;

    [SerializeField]
    float zoomedOutFOV = 40f;

    [SerializeField]
    float zoomedInFOV = 20f;

    [SerializeField]
    float RotationSpeedOnZoom = 0.5f;

    bool zoomedIn = false;

    FirstPersonController firstPersonController;

    float initialRotationSpeed;

    private void Start()
    {
        firstPersonController = FindObjectOfType<FirstPersonController>();
        initialRotationSpeed = firstPersonController.RotationSpeed;
    }

    private void Update()
    {
        // Right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedIn)
            {
                ResetZoom();
            }
            else
            {
                firstPersonController.RotationSpeed = RotationSpeedOnZoom;
                zoomedIn = true;
                fpsCamera.m_Lens.FieldOfView = zoomedInFOV;
            }
        }
    }

    private void OnDisable()
    {
        ResetZoom();
    }

    public void ResetZoom()
    {
        zoomedIn = false;
        fpsCamera.m_Lens.FieldOfView = zoomedOutFOV;
        firstPersonController.RotationSpeed = initialRotationSpeed;
    }
}
