using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GroundChaserEnemyAI : MonoBehaviour
{
    NavMeshAgent nma;
    GameObject[] players;
    GameObject target;
    float smallestDistance;
    [SerializeField] float stoppingDistance;
    [SerializeField] float enemySpeed;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        //get nearest player, set to target, move to target
        smallestDistance = 99999999f;
        if (players.Length > 1)
        {
            for (int i = 0; i < players.Length -1; i++)
            {
                float distance = Vector3.Distance(players[i].transform.position, transform.position);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    target = players[i];
                }
            }
        }
        else
        {
            target = players[0];
        }

        if (target != null)
            nma.destination = target.transform.position;

        if (Vector3.Distance(target.transform.position, transform.position) <= stoppingDistance)
        {
            nma.speed = 0f;
        }
        else
        {
            nma.speed = enemySpeed;
        }
    }
}
