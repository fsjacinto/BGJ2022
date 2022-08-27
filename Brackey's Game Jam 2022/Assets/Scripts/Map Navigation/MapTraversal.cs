using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTraversal : MonoBehaviour
{
    [SerializeField] private Transform enterToRoom;
    [SerializeField] private PlayerFace playerFaceTo;

    [SerializeField] private bool isColliding = false;

    private Transform player;
    private Transform enemy;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponentInChildren<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isColliding)
        {
            StartCoroutine(PlayerTraverseLocation());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = true;
        }

        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.transform;
            StartCoroutine(EnemyTraverseLocation());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    private IEnumerator PlayerTraverseLocation()
    {
        yield return new WaitForSeconds(0.5f);
        playerMovement.PlayerFaceTo(playerFaceTo);
        player.position = enterToRoom.position;
    }

    private IEnumerator EnemyTraverseLocation()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.position = enterToRoom.position;
        enemy.GetComponent<EnemyAI>().traversed = true;
    }

}
