using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public List<CheckPoint> checkpoints = new List<CheckPoint>();

    public int currentTargetIndex;
    [HideInInspector]
    public CheckPoint currentpoint;

    private GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
      
    }

    private void Awake()
    {
        currentTargetIndex = 1;
    }

    public void TriggerCheckPoint()
    {
        if (currentTargetIndex == 0)
        {
            // next lap
            manager.TriggerLap();
        }

        currentTargetIndex++;

        if (currentTargetIndex >= checkpoints.Count || currentTargetIndex < 0)
        {
            currentTargetIndex = 0;
        }

        foreach (CheckPoint check in checkpoints) // disable all checkpoint objs
        {
            check.gameObject.SetActive(false);
        }

        currentpoint = checkpoints[currentTargetIndex];

        checkpoints[currentTargetIndex].gameObject.SetActive(true);
    }

    public void OnDrawGizmos()
    {
        if(checkpoints.Count != 0)
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < checkpoints.Count; i++)
            {
                if (i == 0)
                {
                    Gizmos.DrawLine(checkpoints[i].transform.position, checkpoints[checkpoints.Count - 1].transform.position);
                }
                else
                {
                    Gizmos.DrawLine(checkpoints[i].transform.position, checkpoints[i - 1].transform.position);
                }
            }

            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(checkpoints[currentTargetIndex].transform.position, .1f);
        }
    }
}
