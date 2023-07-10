using UnityEngine;

public class EnemyFlashlight : MonoBehaviour
{
    [SerializeField] private GameObject lightGO;
    public float rotSpeed = 250;
    public float damping = 10;

    public void RotateToDirection(Vector3 direction) {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        lightGO.transform.rotation = Quaternion.RotateTowards(lightGO.transform.rotation, toRotation, rotSpeed * Time.deltaTime);
    }
}
