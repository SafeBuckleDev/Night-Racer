using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointManager cManager;

    private void Start()
    {
        cManager = FindObjectOfType<CheckPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cManager.TriggerCheckPoint();
        }
    }
}
