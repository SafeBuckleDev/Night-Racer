using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterTree : MonoBehaviour
{
    [SerializeField] private GameObject[] locations;


    public Animator monsterTreeAnim;

    private int currentLOC;

    [SerializeField] private float speed;
    void Update()
    {
        if (Vector3.Distance(locations[currentLOC].transform.position, transform.position) < .2f)
        {
            currentLOC++;
            if (currentLOC >= locations.Length)
            {
                currentLOC = 0;
            }

        }
        transform.position = Vector3.MoveTowards(transform.position, locations[currentLOC].transform.position, Time.deltaTime * speed);
        //monsterTreeAnim.SetBool("walking", true);
    }
}
