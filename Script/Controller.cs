using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Controller : MonoBehaviour
{
    float speed = 6;
    float rotspeed = 80;
    float gravity = 0;
    float rot = 0f;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("Condition", 1);
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetInteger("Condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }         
            rot += CrossPlatformInputManager.GetAxis("Horizontal") * rotspeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rot, 0);
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }
    }
}
