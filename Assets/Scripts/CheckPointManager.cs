using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointElement
{
    [HideInInspector]
    public string name = "CheckPoint";

    public CheckPoint checkPoint;
}

public class CheckPointManager : MonoBehaviour
{
    public List<CheckpointElement> checkpoints = new List<CheckpointElement>();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerCheckPoint();
        }
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

        currentpoint = checkpoints[currentTargetIndex].checkPoint;
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
                    Gizmos.DrawLine(checkpoints[i].checkPoint.transform.position, checkpoints[checkpoints.Count - 1].checkPoint.transform.position);
                }
                else
                {
                    Gizmos.DrawLine(checkpoints[i].checkPoint.transform.position, checkpoints[i - 1].checkPoint.transform.position);
                }
            }

            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(checkpoints[currentTargetIndex].checkPoint.transform.position, .1f);
        }
    }
}
