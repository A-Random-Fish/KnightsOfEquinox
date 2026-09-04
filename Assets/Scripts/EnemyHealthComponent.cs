using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class EnemyHealthComponent : MonoBehaviour
{
    Rigidbody rb;
    int health;
    [SerializeField] float iframes;
    [SerializeField] int maxHealth;
    NavMeshAgent nma;
    [SerializeField] int defense;
    [SerializeField] float kbDuration;
    [SerializeField] float kbForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        nma = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        iframes -= Time.deltaTime;
    }

    public void Damage(int damage)
    {
        if (iframes <= 0)
        {
            int damageToDeal = damage - defense;
            health -= damageToDeal;
            iframes = 0.33f;
            
            if (health <= 0 )
            {
                Death();
            }
        }
    }

    public IEnumerator KnockbackEnemy(Transform kbLocation)
    {
        nma.enabled = false;
        rb.isKinematic = false;
        rb.AddForce(transform.position - kbLocation.position * kbForce + Vector3.up * kbForce/2);
        yield return new WaitForSeconds(kbDuration);
        nma.Warp(transform.position);
        nma.enabled = true;
        rb.isKinematic = true;
    }

    private void Death()
    {
        Destroy(gameObject);
    }


}
