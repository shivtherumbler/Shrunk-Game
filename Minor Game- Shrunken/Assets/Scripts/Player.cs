using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    public float DirectionDampTime = 0.25f;
    public bool ApplyGravity = true;

    private CharacterController characterController;

    public float Speed = 2.0f;

    public float RotationSpeed = 240.0f;

    private Vector3 moveDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (v < 0)
            v = 0;

        transform.Rotate(0, h * RotationSpeed * Time.deltaTime, 0);

        if(characterController.isGrounded)
        {
            bool move = (v > 0 || h != 0);

            animator.SetBool("Run", move);

            moveDir = Vector3.forward * v;

            moveDir = transform.TransformDirection(moveDir);
            moveDir *= Speed;

        }

        //animator.SetFloat("Speed", h * h + v * v);
        //animator.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);

        if (Input.GetButton("Jump")) animator.SetBool("Jump", true);
        else
        {
            animator.SetBool("Jump", false);
        }


        characterController.Move(moveDir * Time.deltaTime);
    }
}
