using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] List<GameObject> bodyObjects;
    [SerializeField] Material normalMat;
    [SerializeField] Material hitFlashMat;

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
            StartCoroutine("HitFlash");
            
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

    private IEnumerator HitFlash()
    {
        for (int i = 0; i < bodyObjects.Count; i++)
        {
            if (bodyObjects[i].GetComponent<MeshRenderer>() != null)
                bodyObjects[i].GetComponent<MeshRenderer>().material = hitFlashMat;
            else if (bodyObjects[i].GetComponent<SkinnedMeshRenderer>() != null)
                bodyObjects[i].GetComponent<SkinnedMeshRenderer>().material = hitFlashMat;
        }

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < bodyObjects.Count; i++)
        {
            if (bodyObjects[i].GetComponent<MeshRenderer>() != null)
                bodyObjects[i].GetComponent<MeshRenderer>().material = normalMat;
            else if (bodyObjects[i].GetComponent<SkinnedMeshRenderer>() != null)
                bodyObjects[i].GetComponent<SkinnedMeshRenderer>().material = normalMat;
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }


}
