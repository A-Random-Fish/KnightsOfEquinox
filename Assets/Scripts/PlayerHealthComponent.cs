using UnityEngine;
using System.Collections;

public class PlayerHealthComponent : MonoBehaviour
{ 
    int health;
    [SerializeField] float iframes;
    [SerializeField] int maxHealth;
    [SerializeField] int defense;
    [SerializeField] float kbDuration;

    void Start()
    {
        health = maxHealth;
    }

    public void PlayerDamage(int damage)
    {
        int damageToDeal = damage - defense;
        health -= damageToDeal;
        if (health <= 0 )
        {
            Death();
        }
    }

    private IEnumerator KnockbackPlayer()
    {
        yield return new WaitForSeconds(kbDuration);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
