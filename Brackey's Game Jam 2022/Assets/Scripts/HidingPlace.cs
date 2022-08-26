using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    private Transform position;
    private bool hideEnabled = false;
    private bool isOccupied = false;
    private GameObject playerGO;
    private Vector3 playerPrevPosition;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!hideEnabled) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOccupied)
            {
                Debug.Log("Funny");
                isOccupied = true;
                // move player inside
                //playerGO.transform.position = transform.position;

                // hide player sprite
                playerGO.transform.GetChild(0).gameObject.SetActive(false);

                // change state
                playerGO.GetComponent<PlayerController>().ChangePlayerState(PlayerController.PlayerState.Hiding);
            }
            else
            {
                isOccupied = false;
                // Player goes out
                playerGO.transform.GetChild(0).gameObject.SetActive(true);

                // show player sprite
                //playerGO.transform.position = playerPrevPosition;

                // change state
                playerGO.GetComponent<PlayerController>().ChangePlayerState(PlayerController.PlayerState.Exploring);
            }


        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Enemy"))
            Debug.Log("EnterEnemy");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enter");
            playerGO = collision.gameObject;
            playerPrevPosition = playerGO.transform.position;


            // show prompt

            // enable hiding
            hideEnabled = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Exit");
            // show prompt

            // disable hiding
            hideEnabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Enter");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enter");
        }
    }
}
