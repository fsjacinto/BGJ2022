using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeLocation : MonoBehaviour
{
    [SerializeField] private GameObject vCamera;
    [SerializeField] private Location location;
    [SerializeField] private List<MapTraversal> mapTraversals;

    private PlayerMovement playerMovement;
    private EnemyAI enemyAI;
    private Light enemyLight;
    private SpriteRenderer enemySpriteRenderer;

    private void Awake()
    {
        vCamera.SetActive(false);
        enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
        enemySpriteRenderer = enemyAI.GetComponentInChildren<SpriteRenderer>();
        enemyLight = GameObject.FindGameObjectWithTag("EnemyLight").GetComponent<Light>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        enemyLight.enabled = false;
        enemySpriteRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            vCamera.SetActive(true);
            GameManager.instance.virtualCamera = vCamera.GetComponent<CinemachineVirtualCamera>();
            playerMovement.playerLocation = location;

            if (enemyAI.enemyLocation == playerMovement.playerLocation)
            {
                enemyLight.enabled = true; 
                enemySpriteRenderer.enabled = true;
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyAI>().ChangeLocation(location);

            if (collision.GetComponent<EnemyAI>().enemyLocation == playerMovement.playerLocation)
            {
                enemyLight.enabled = true;
                enemySpriteRenderer.enabled = true;
            }

            if (collision.GetComponent<EnemyAI>().enemyLocation == location)
            {
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

            StartCoroutine(ToggleEnemy());
        }

        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(ToggleEnemy());

            if (collision.GetComponent<EnemyAI>().enemyLocation == location)
            {
                foreach (MapTraversal mt in mapTraversals)
                {
                    StartCoroutine(mt.ToggleEnemyTraversal(false));
                }
            }
        }
    }

    IEnumerator ToggleEnemy()
    {
        yield return new WaitForSeconds(0.1f);
        if (enemyAI.enemyLocation != playerMovement.playerLocation)
        {
            enemyLight.enabled = false;
            enemySpriteRenderer.enabled = false;
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