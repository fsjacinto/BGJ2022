using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTraversal : MonoBehaviour
{
    [SerializeField] private Transform enterToRoom;
    private Transform player;
    private Transform enemy;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            StartCoroutine(PlayerTraverseLocation());
        }
        /*
        else if (collision.CompareTag("Enemy"))
        {
            if (enemy == null)
            {
                enemy = collision.transform;    //cache enemy
            }
            StartCoroutine(EnemyTraverseLocation());
        }
        */
    }

    private IEnumerator PlayerTraverseLocation()
    {
        yield return new WaitForSeconds(0.5f);
        player.position = enterToRoom.position;
    }

    private IEnumerator EnemyTraverseLocation()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.position = enterToRoom.position;
    }
}
