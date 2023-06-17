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

            return;
        }

        if (bulletShot == BulletShotType.Radial)
        {
            Vector3 firingDir = transform.forward;
            Quaternion bulletRotation = transform.rotation; 
            float rotationAroundPerBullet = 360 / numOfBullets;

            Instantiate(bulletPrefab, transform.position + (firingDir * 3), bulletRotation);

            for (int i = 1; i < numOfBullets; i++)
            {
                float sin = Mathf.Sin(rotationAroundPerBullet * Mathf.Deg2Rad);
                float cos = Mathf.Cos(rotationAroundPerBullet * Mathf.Deg2Rad);

                float tx = firingDir.x;
                float ty = firingDir.y;
                firingDir.x = (cos * tx) - (sin * ty);
                firingDir.y = (sin * tx) + (cos * ty);

                bulletRotation = Quaternion.Euler(bulletRotation.eulerAngles.x, bulletRotation.eulerAngles.y + rotationAroundPerBullet, bulletRotation.eulerAngles.z);

                Instantiate(bulletPrefab, transform.position + (firingDir * 3), bulletRotation);
            }

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
