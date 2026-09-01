using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GroundChaserEnemyAI : MonoBehaviour
{
    private NavMeshAgent nma;
    GameObject[] players;
    GameObject target;
    float smallestDistance;
    [SerializeField] float kbDuration;

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
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("PlayerHitbox"))
        {
            StartCoroutine("KnockbackEnemy");
        }
    }

    private IEnumerator KnockbackEnemy()
    {
        nma.enabled = false;
        yield return new WaitForSeconds(kbDuration);
        nma.enabled = true;
    }
}
