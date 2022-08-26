using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody playerRB;
    private BoxCollider2D playerBC;
    //[SerializeField] private float speed;

    //[SerializeField] private LayerMask groundLayer;

    //private Animator anim;

    private CharacterController controller;
    [SerializeField] private float speed;
    private Vector3 playerVelocity;
    float gravity = 1f;
    float vSpeed = 0f;

    PlayerState playerState;

    private void Awake()
    {
        // Get references
        //playerRB = GetComponent<Rigidbody>();
        //playerBC = GetComponent<BoxCollider2D>();
        //anim = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();

    }


    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");

        //// Player horizontal movement
        //playerRB.velocity = new Vector3(-horizontalInput * speed, playerRB.velocity.y, playerRB.velocity.z);
        ////playerRB.
        ///

        //-------------------------------
        //float moveLR = Input.GetAxisRaw("Horizontal");
        //float moveFB = Input.GetAxisRaw("Vertical");

        //Vector3 direction = new Vector3(-moveLR, 0f, -moveFB).normalized;
        //Debug.Log(direction.magnitude);

        //if(direction.magnitude > 0.01f)
        //{
        //    controller.Move(direction * speed * Time.deltaTime);
        //}
        //-------------------------------
        //if (playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        if (playerState == PlayerState.Hiding) return;



        float moveLR = Input.GetAxisRaw("Horizontal");
        float moveFB = Input.GetAxisRaw("Vertical");

        if (moveLR == 0f && moveFB == 0f)
        {
            animator.SetTrigger("Idle");
        }

        //if(moveLR > 0.1f)
        //{
        //    animator.SetTrigger("RunRight");
        //}



        if(moveLR !=0f && moveFB != 0f)
        {
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Horizontal", moveLR);
        }
        else
        {
            //if(moveLR != 0f)
            animator.SetFloat("Horizontal", moveLR);

            //if(moveFB != 0f)
            animator.SetFloat("Vertical", moveFB);
        }


        Vector3 move = new Vector3(-moveLR, 0f, -moveFB).normalized;
        controller.Move(move * speed * Time.deltaTime);

       //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;

        }

        //controller.Move(playerVelocity * Time.deltaTime);

        // Gravity
        vSpeed -= gravity * Time.deltaTime;
        move.y = vSpeed;
        controller.Move(move * Time.deltaTime);

    }

    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }

    public void ChangePlayerState(PlayerState state)
    {
        playerState = state;
    }


    public enum PlayerState
    {
        Exploring,
        Hiding
    }
}
