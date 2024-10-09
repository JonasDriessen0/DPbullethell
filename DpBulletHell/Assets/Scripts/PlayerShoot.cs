using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootTransform;
    [SerializeField] private Transform bulletPool;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float destroyTime;

    [SerializeField] private List<GameObject> disabledBullets;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bulletObject;
        if (disabledBullets.Count > 0 && !disabledBullets[0].activeSelf)
        {
            bulletObject = disabledBullets[0];
            bulletObject.SetActive(true);
            bulletObject.transform.position = shootTransform.position;
            bulletObject.transform.rotation = shootTransform.rotation;
            disabledBullets.RemoveAt(0);
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
        disabledBullets.Add(bulletObject);
        Debug.Log("add bullet to list");
    }
}