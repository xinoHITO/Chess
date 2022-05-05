using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera VirtualCam;

    // Start is called before the first frame update
    void Start()
    {
        VirtualCam = FindObjectOfType<CinemachineVirtualCamera>();
        if (VirtualCam == null)
        {
            Debug.LogError("No cinemachine camera found");
            return;
        }
        VirtualCam.Follow = transform;
    }

}
