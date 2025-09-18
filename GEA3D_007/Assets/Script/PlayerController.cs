using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    public float jumpPower = 5f;

    public float gravity = -9.81f;

    

    public CinemachineVirtualCamera virtualCamera;

    public float rotationSpeed = 10f;

    public CinemachinePOV pov;


    private CharacterController controller;

    private Vector3 velocity;

    public bool isGrounded;

    private CinemachineSwitche camSwitcher;






    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        camSwitcher = FindObjectOfType<CinemachineSwitche>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if(isGrounded = controller.isGrounded)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 camForward = virtualCamera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = virtualCamera.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized;
        controller.Move(move * speed * Time.deltaTime);

        float camraYaw = pov.m_HorizontalAxis.Value;
        Quaternion targetRot = Quaternion.Euler(0f, camraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        if (camSwitcher != null && camSwitcher.usingFreeLook)
        {
            // FreeLook 모드면 이동 막기
            return;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 15f;
            virtualCamera.m_Lens.FieldOfView = 80f;
            

        }
        else
        {
            speed = 5f;
            virtualCamera.m_Lens.FieldOfView = 60;
        }


    }

    

}
