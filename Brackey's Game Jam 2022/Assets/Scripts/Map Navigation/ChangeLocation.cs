using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocation : MonoBehaviour
{
    [SerializeField] private GameObject vCamera;
    [SerializeField] private Location location;
    [SerializeField] private List<MapTraversal> mapTraversals;

    private EnemyAI enemyAI;
    private Light enemyLight;
    private SpriteRenderer enemySpriteRenderer;

    private void Awake()
    {
        vCamera.SetActive(false);
        enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
        enemySpriteRenderer = enemyAI.GetComponentInChildren<SpriteRenderer>();
        enemyLight = GameObject.FindGameObjectWithTag("EnemyLight").GetComponent<Light>();
        
        enemyLight.enabled = false;
        enemySpriteRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(location);
        if (collision.CompareTag("Player"))
        {
            vCamera.SetActive(true);

            if (enemyAI.enemyLocation == location)
            {
                enemyLight.enabled = true; 
                enemySpriteRenderer.enabled = true;
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyAI>().ChangeLocation(location);

            if(collision.GetComponent<EnemyAI>().enemyLocation == location)
            {
                enemyLight.enabled = true;
                enemySpriteRenderer.enabled = true;

                foreach (MapTraversal mt in mapTraversals)
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
            enemyLight.enabled = false;
            enemySpriteRenderer.enabled = false;
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
    Kitchen,
    Hallway1F,
    Hallway2F,
    Storage,
    Bedroom
}