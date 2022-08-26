using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeLocation : MonoBehaviour
{
    [SerializeField] private GameObject location;
    [SerializeField] private CinemachineVirtualCamera vCam;

    private void OnTriggerEnter(Collider collision)
    {
        // Change Location
        if (collision.CompareTag("Player"))
        {
            if (vCam.LookAt == null)
            {
                vCam.LookAt = collision.transform;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            location.SetActive(false);
        }
    }
}
