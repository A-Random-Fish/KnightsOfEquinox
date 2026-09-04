using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyHealthComponent : MonoBehaviour
{
    int health;
    [SerializeField] float iframes;
    [SerializeField] int maxHealth;
    NavMeshAgent nma;
    [SerializeField] int defense;
    [SerializeField] float kbDuration;

    void Start()
    {
        health = maxHealth;
        nma = GetComponent<NavMeshAgent>();
    }

    public void Damage(int damage)
    {
        int damageToDeal = damage - defense;
        health -= damageToDeal;
        
        if (health <= 0 )
        {
            Death();
        }
    }

    private IEnumerator KnockbackEnemy()
    {
        nma.enabled = false;
        yield return new WaitForSeconds(kbDuration);
        nma.enabled = true;
    }

    private void Death()
    {
        Destroy(gameObject);
    }


}
