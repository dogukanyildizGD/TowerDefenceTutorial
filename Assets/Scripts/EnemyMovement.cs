using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavePointIndex = 0;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        target = WayPoints.points[0]; // ilk hedef belirlendi.
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position; // Hedef yolu belirlendi.
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(target.position, transform.position) < 0.4f)
        {
            GetNextWavePoint();
        }

        // slow uygulanmış ise hızı her framede
        // startSpeed'e eşitlemek için yazıldı
        enemy.speed = enemy.startSpeed;
    }

    private void GetNextWavePoint()
    {
        if (wavePointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavePointIndex++;
        target = WayPoints.points[wavePointIndex];
    }

    private void EndPath()
    {
        PlayerStats.Live--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
