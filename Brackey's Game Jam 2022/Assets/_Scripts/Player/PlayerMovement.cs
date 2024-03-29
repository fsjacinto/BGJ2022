using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;

    [SerializeField] private float playerSpeed;
    public Animator playerAnimator;

    [Header("Player Stairs")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.2f;
    [SerializeField] float stepSmooth = 0.1f;

    private float mH = 0f;
    private float mV = 0f;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        stepRayUpper.transform.position = new Vector3(stepRayLower.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentState != GameState.Exploration)
        {
            mV = 0f;
            mH = 0f;
            return;
        }
        // Get input
        mH = -(Input.GetAxisRaw("Horizontal"));
        mV = -(Input.GetAxisRaw("Vertical"));

        Vector2 moveInput = new Vector2(mH, mV).normalized;
        
        // Apply the movement vector to the current position, which is
        // multiplied by deltaTime and speed for a smooth MovePosition
        playerRB.velocity = new Vector3(moveInput.x * playerSpeed, playerRB.velocity.y, moveInput.y * playerSpeed);

        //Climbing up stairs
        StepClimb();
    }

    private void Update()
    {
        // Animation
        if (mH == 0f && mV == 0f)
        {
            playerAnimator.SetTrigger("Idle");
        }

        if (mH != 0f && mV != 0f)
        {
            playerAnimator.SetFloat("Vertical", 0f);
            playerAnimator.SetFloat("Horizontal", mH);

            //Play walking sfx
            if (!AudioManager.Instance.GetSource("Footstep").isPlaying)
            {
                AudioManager.Instance.Play("Footstep");
            }
        }
        else
        {
            playerAnimator.SetFloat("Horizontal", mH);
            playerAnimator.SetFloat("Vertical", mV);

            if(mH != 0f || mV != 0f)
            {             //Play walking sfx
                if (!AudioManager.Instance.GetSource("Footstep").isPlaying)
                {
                    AudioManager.Instance.Play("Footstep");
                }
            }

        }
    }

    private void StepClimb()
    {
        RaycastHit hitLower;

        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.back), out hitLower, 0.2f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.back), out hitUpper, 0.2f) && mV < 0)
            {
                playerRB.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }

    public void PlayerFaceTo(FaceDirection faceDirection)
    {
        if (faceDirection == FaceDirection.Front)
        {
            playerAnimator.SetTrigger("FaceFront");
        }
        else if (faceDirection == FaceDirection.Left)
        {
            playerAnimator.SetTrigger("FaceLeft");
        }
        else if (faceDirection == FaceDirection.Right)
        {
            playerAnimator.SetTrigger("FaceRight");
        }
    }
}