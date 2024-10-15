using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    public Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    private float fireCountDown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int takeOverDamage = 30;
    public float slowAmount = .5f;

    [Header("Unity Setup Files")]

    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public Transform firePoint;  
   
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        // Target yani enemyleri tag'a göre bul ve listelenedi.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // En kısa mesafeyi bulmak için Mathf.Infinity kullanıldı.
        // Mathf.Infinity limiti sonsuza götürür.
        float sortestDistance = Mathf.Infinity;
        // En yakındaki enemy için bir GameObject yazıldı.
        GameObject nearestEnemy = null;
        // Her bir enemy için for
        foreach (GameObject enemy in enemies)
        {
            // Enemy'e olan uzaklığı Vector3 olarak alındı.
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < sortestDistance)
            {
                sortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && sortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    impactEffect.Stop();
                    lineRenderer.enabled = false;
                    impactLight.enabled = false;
                }
                    
            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountDown < 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }
                
    }

    private void Laser()
    {

        targetEnemy.TakeDamage(takeOverDamage * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        // Laser grafik kısmı.
        if (!lineRenderer.enabled)
        {            
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
                          
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void LockOnTarget()
    {
        // Turrent ile hedefin arasındaki uzaklık 3d vektör cinsinden.
        Vector3 dir = target.position - transform.position;
        // Turrent'in hedefi takip etmesi için yazıldı.
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
