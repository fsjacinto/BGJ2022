using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTraversal : MonoBehaviour
{
    [SerializeField] private Transform enterToRoom;
    [SerializeField] private PlayerFace playerFaceTo;
    [SerializeField] private Location colliderLocation;
    [SerializeField] private DialogueTrigger noAccessDialogue;
    [SerializeField] private List<int> prereqList;

    [SerializeField] private bool isColliding = false;
    [SerializeField] private bool hasEnemyStayedLong = false;
    private bool canAccess;
    private bool isTraversing = false;
    //private bool isConvo;

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
            if (canAccess && !isTraversing)
            {
                isTraversing = true;
                StartCoroutine(PlayerTraverseLocation());
            }
            else if (GameManager.instance.currentState == GameState.Exploration)
            {
                Debug.Log(GameManager.instance.currentState);
                noAccessDialogue.StartDialogue();
            }
            //if(GameManager.instance.dialogueGO.activeSelf == false)
        }

        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = true;

            canAccess = GameManager.instance.CheckPrereqTasks(prereqList);
            //GameObject levelManager = GameObject.FindWithTag("LevelManager");
            //levelManager.GetComponent<Level>
            //canAccess = Ga

        }

        if (collision.CompareTag("Enemy"))
        {
            if (hasEnemyStayedLong)
            {
                enemy = collision.transform;
                StartCoroutine(EnemyTraverseLocation());
            }
            //else
            //{
            //    StartCoroutine(AllowEnemyTraversal());
            //}
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
        GameManager.instance.TriggerFadeTransition(0.5f);
        yield return new WaitForSeconds(0.5f);
        playerMovement.PlayerFaceTo(playerFaceTo);
        player.position = enterToRoom.position;
        isTraversing = false;
    }

    private IEnumerator EnemyTraverseLocation()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.position = enterToRoom.position;
        enemy.GetComponent<EnemyAI>().traversed = true;

        if(enemy.GetComponent<EnemyAI>().enemyLocation != colliderLocation)
        {
            hasEnemyStayedLong = false;
        }
        //hasEnemyStayedLong = false;

    }

    public IEnumerator ToggleEnemyTraversal(bool isAllow)
    {
        yield return new WaitForSeconds(0.5f);
        hasEnemyStayedLong = isAllow;
    }

}