using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    //private Transform position;
    //private bool hideEnabled = false;
    //private bool isOccupied = false;

    //private Vector3 playerPrevPosition;

    private bool isColliding;
    [SerializeField] private SpriteRenderer playerSR;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!hideEnabled) return;
        
        if (Input.GetKeyDown(KeyCode.Space) && isColliding)
        {
            if(GameManager.instance.currentState == GameState.Exploration)
            {
                //hide player
                playerSR.enabled = false;
                playerCollider.enabled = false;
                playerRB.constraints = RigidbodyConstraints.FreezePosition;
                GameManager.instance.currentState = GameState.Hiding;

            }

            else if(GameManager.instance.currentState == GameState.Hiding)
            {
                // show player
                playerSR.enabled = true;
                playerCollider.enabled = true;
                playerRB.constraints = ~RigidbodyConstraints.FreezePosition;
                GameManager.instance.currentState = GameState.Exploration;
            }


            //if (!isOccupied)
            //{
            //    Debug.Log("Funny");
            //    isOccupied = true;
            //    // move player inside
            //    //playerGO.transform.position = transform.position;

            //    // hide player sprite
            //    playerGO.transform.GetChild(0).gameObject.SetActive(false);

            //    // change state
            //    playerGO.GetComponent<PlayerController>().ChangePlayerState(PlayerController.PlayerState.Hiding);
            //}
            //else
            //{
            //    isOccupied = false;
            //    // Player goes out
            //    playerGO.transform.GetChild(0).gameObject.SetActive(true);

            //    // show player sprite
            //    //playerGO.transform.position = playerPrevPosition;

            //    // change state
            //    playerGO.GetComponent<PlayerController>().ChangePlayerState(PlayerController.PlayerState.Exploring);
            //}


        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //if (collision.CompareTag("Enemy"))
        //    Debug.Log("EnterEnemy");
        if (collider.CompareTag("Player"))
        {
            //   Debug.Log("Enter");

            isColliding = true;
            //playerGO = collider.gameObject;
            //playerPrevPosition = playerGO.transform.position;


            // show prompt

            // enable hiding
            //hideEnabled = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isColliding = false;
            Debug.Log("Exit");

         //   hideEnabled = false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Debug.Log("Enter");
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Enter");
    //    }
    //}
}
