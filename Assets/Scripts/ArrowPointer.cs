using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    private CheckPointManager cManager;

    private void Start()
    {
        cManager = FindObjectOfType<CheckPointManager>();
    }

    private void LateUpdate()
    {
        transform.LookAt(cManager.checkpoints[cManager.currentTargetIndex].transform);
    }
}
