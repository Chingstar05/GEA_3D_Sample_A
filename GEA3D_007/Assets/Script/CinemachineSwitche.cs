using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitche : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public CinemachineFreeLook freeLookCam;

    public bool usingFreeLook = false;

    public CinemachineSwitche camSwitche;



    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.Priority = 10;
        freeLookCam.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //¿ìÅ¬¸¯
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook)
            {
                freeLookCam.Priority = 20;
                virtualCamera.Priority = 0;
                if (camSwitche != null && camSwitche.usingFreeLook)
                    return;
            }
            else
            {
                virtualCamera.Priority = 20;
                freeLookCam.Priority = 0;
            }
        }
        
    }
}
