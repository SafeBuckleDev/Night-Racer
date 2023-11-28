using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterTree : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    private int currentwaypointindex;

    [SerializeField] private float speed = 2f;
    void Update()
    {
        if (Vector3.Distance(waypoints[currentwaypointindex].transform.position, transform.position) < .2f)
        {
            currentwaypointindex++;
            if (currentwaypointindex >= waypoints.Length)
            {
                currentwaypointindex = 0;
            }

        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentwaypointindex].transform.position, Time.deltaTime * speed);
    }
}
