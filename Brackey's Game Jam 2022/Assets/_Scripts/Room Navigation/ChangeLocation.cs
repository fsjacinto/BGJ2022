using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

public class ChangeLocation : MonoBehaviour
{
    [SerializeField] private GameObject vCamera;
    [SerializeField] private Location location;

    private EnemyAI enemyAI;

    private void Awake()
    {
        vCamera.SetActive(false);
        enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            vCamera.SetActive(true);
            GameManager.Instance.virtualCamera = vCamera.GetComponent<CinemachineVirtualCamera>();
            LocationManager.Instance.ChangePlayerLocation(location);
        }

        if (collision.CompareTag("Enemy"))
        {
            LocationManager.Instance.ChangeEnemyLocation(location);
        }

        HandleEnemy();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            vCamera.SetActive(false);
        }

        HandleEnemy();
    }

    private async void HandleEnemy() {

        if (enemyAI.enemyState != EnemyState.Patrolling) return;

        await Task.Delay(100);
        enemyAI.ToggleEnemyVisibility(LocationManager.Instance.isSameLocation());
    }
}