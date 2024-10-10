using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform shootTransform;
    [SerializeField] private float destroyTime;

    private float lastShot;
    [SerializeField] private float timeBetweenShots = 0.5f;

    private void Update()
    {
        if (Time.time > lastShot + timeBetweenShots)
        {
            lastShot = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bulletObject = BulletPool.instance.GetBullet();
        bulletObject.transform.position = shootTransform.position;
        bulletObject.transform.rotation = shootTransform.rotation;

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Shoot(shootTransform.up);
        }

        StartCoroutine(DeactivateBullet(bulletObject));
    }

    IEnumerator DeactivateBullet(GameObject bulletObject)
    {
        yield return new WaitForSeconds(destroyTime);
        BulletPool.instance.ReturnBullet(bulletObject);
    }
}