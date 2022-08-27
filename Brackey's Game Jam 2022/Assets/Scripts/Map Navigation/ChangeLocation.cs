using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocation : MonoBehaviour
{
    [SerializeField] private GameObject vCamera;
    [SerializeField] private Location location;
    [SerializeField] private List<MapTraversal> mapTraversals;

    private void Awake()
    {
        vCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(location);
        if (collision.CompareTag("Player"))
        {
            vCamera.SetActive(true);
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyAI>().enemyLocation = location;

            if(collision.GetComponent<EnemyAI>().enemyLocation == location)
            {
                foreach(MapTraversal mt in mapTraversals)
                {
                    StartCoroutine(mt.ToggleEnemyTraversal(true));
                }
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            vCamera.SetActive(false);
        }

        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<EnemyAI>().enemyLocation == location)
            {
                foreach (MapTraversal mt in mapTraversals)
                {
                    StartCoroutine(mt.ToggleEnemyTraversal(false));
                }
            }
        }
    }
}

public enum Location
{
    Basement,
    Livingroom,
    Kitchen
}