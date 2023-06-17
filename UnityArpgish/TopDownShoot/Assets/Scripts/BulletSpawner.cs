using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletShotType
{
    Normal,
    Cone,
    Radial
}

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float timeBetweenSpawns = 1f;
    float timer = 0;
    [SerializeField]
    int numOfBullets = 1;
    [SerializeField]
    BulletShotType bulletShot = BulletShotType.Normal;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawns)
        {
            Fire();
            timer = 0;
        }
    }

     void Fire()
     {
        if (bulletShot == BulletShotType.Normal)
        {
            StartCoroutine( NormalBurstCoroutine());

            return;
        }

        if (bulletShot == BulletShotType.Cone)
        {
            Vector3 firingDir = transform.forward;
            float rotationPerBullet = Mathf.Deg2Rad * (360 / numOfBullets);
            for (int i = 0; i < numOfBullets; i++)
            {
                if (i < numOfBullets / 2)
                {
                    Vector3.RotateTowards(firingDir, -transform.forward, rotationPerBullet, 0);
                    continue;
                }
                
                Vector3.RotateTowards(firingDir, transform.forward, rotationPerBullet, 0);
            }
            return;
        }

        if (bulletShot == BulletShotType.Radial)
        {
            return;
        }

        Debug.LogError("This statement should not execute. Case was missed.");
     }

    IEnumerator NormalBurstCoroutine()
    {
        for (int i = 0; i < numOfBullets; i++)
        {
            Instantiate(bulletPrefab, transform);
            yield return new WaitForSeconds(0.5f);
        }
    }
    

}
