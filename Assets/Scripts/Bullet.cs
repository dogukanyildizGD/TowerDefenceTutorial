using System;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float Speed = 70f;
    public int damage = 50;

    public GameObject impactEffect;

    public float explotionRadius = 0f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    private void HitTarget()
    {

        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explotionRadius > 0)
        {
            Explotions();
        }
        else
        {
            Damage(target);
        }
              
        Destroy(gameObject);
    }

    private void Explotions()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explotionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
        {
            e.TakeDamage(damage);
        }       
 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explotionRadius);
    }
}
