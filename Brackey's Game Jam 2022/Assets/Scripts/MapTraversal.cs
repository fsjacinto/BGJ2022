using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTraversal : MonoBehaviour
{
    [SerializeField] private Transform playerEnter;
    private GameObject entity;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            Debug.Log("Enter");
            entity = collision.transform.gameObject;
            entity.transform.position = playerEnter.transform.position;
            StartCoroutine(TraverseLocation());
        
        }
    }

    private IEnumerator TraverseLocation()
    {
        yield return new WaitForSeconds(0.5f);
        entity.transform.position = playerEnter.transform.position;
    }
}
