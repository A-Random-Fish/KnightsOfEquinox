using UnityEngine;

public class HitboxPlayerManager : MonoBehaviour
{
    [SerializeField] int damage;
    EnemyHealthComponent enemyHealth;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyHealth = other.GetComponent<EnemyHealthComponent>();
            if (enemyHealth != null)
                enemyHealth.Damage(damage);
        }   
    }
}
