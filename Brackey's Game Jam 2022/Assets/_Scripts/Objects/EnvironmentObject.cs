using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    [SerializeField] protected bool isPlayerColliding = false;
    protected GameObject collidingGO;

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerColliding = true;
        }
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerColliding = false;
        }
    }
}
