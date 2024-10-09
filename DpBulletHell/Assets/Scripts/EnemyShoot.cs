using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform shootTransform;
    [SerializeField] private Transform bulletPool;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float destroyTime;

    private float lastShot;
    [SerializeField] private float timeBetweenShots = 0.5f;
    
    [SerializeField] private List<GameObject> enemyDisabledBullets;

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
        GameObject bulletObject;
        if (enemyDisabledBullets.Count > 0 && !enemyDisabledBullets[0].activeSelf)
        {
            bulletObject = enemyDisabledBullets[0];
            bulletObject.SetActive(true);
            bulletObject.transform.position = shootTransform.position;
            bulletObject.transform.rotation = shootTransform.rotation;
            enemyDisabledBullets.RemoveAt(0);
            Debug.Log("reuse bullet");
        }
        else
        {
            bulletObject = Instantiate(bulletPrefab, shootTransform.position, shootTransform.rotation, bulletPool);
            Debug.Log("make new bullet");
        }

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
        
        bulletObject.SetActive(false);
        enemyDisabledBullets.Add(bulletObject);
        Debug.Log("add bullet to list");
    }
}