using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Timeline;

public class GroundChaserEnemyAI : MonoBehaviour
{
    NavMeshAgent nma;
    GameObject[] players;
    GameObject target;
    float smallestDistance;
    [SerializeField] float stoppingDistance;
    [SerializeField] float enemySpeed;
    float attackCooldown = 1f;
    [SerializeField] float attackCooldownDuration;
    [SerializeField] GameObject HitboxGameObject;

    void Start()
    {
        HitboxGameObject.GetComponent<Collider>().enabled = false;
        nma = GetComponent<NavMeshAgent>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
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
            if (attackCooldown <= 0f)
            {
                attackCooldown = attackCooldownDuration;
                StartCoroutine("AttackHitboxEnable");
            }
        }
        else
        {
            nma.speed = enemySpeed;
        }
    }

    private IEnumerator AttackHitboxEnable()
    {
            yield return new WaitForSeconds(0.2f);
            HitboxGameObject.GetComponent<BoxCollider>().enabled = true;
            yield return new WaitForSeconds(0.4f);
            HitboxGameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
