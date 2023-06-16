using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    GameObject bulletPrefab;
    float timeBetweenSpawns = 1f;
    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawns)
        {
            Instantiate(bulletPrefab, transform);
            timer = 0;
        }
    }
}
