using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;

    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private Animator animator;

    private float mH = 0f;
    private float mV = 0f;


    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();    
    }

    void FixedUpdate()
    {
        // Get input
        mH = -(Input.GetAxisRaw("Horizontal"));
        mV = -(Input.GetAxisRaw("Vertical"));

       
        // Apply the movement vector to the current position, which is
        // multiplied by deltaTime and speed for a smooth MovePosition
        playerRB.velocity = new Vector3(mH * playerSpeed, playerRB.velocity.y, mV * playerSpeed);
    }

    void Update()
    {
        // Animation
        if (mH == 0f && mV == 0f)
        {
            animator.SetTrigger("Idle");
        }

        if (mH != 0f && mV != 0f)
        {
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Horizontal", mH);
        }
        else
        {
            animator.SetFloat("Horizontal", mH);

            animator.SetFloat("Vertical", mV);
        }
    }
}
