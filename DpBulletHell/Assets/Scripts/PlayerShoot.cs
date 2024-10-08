using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootTransform;
    [SerializeField] private Transform bulletPool;
    [SerializeField] private GameObject bullet;
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
        if (disabledBullets.Count > 0 && disabledBullets[0].activeSelf == false)
        {
            disabledBullets[0].SetActive(true);
            //disabledBullets[0].transform = shootTransform;
            var disabledBullet = disabledBullets[0];
            disabledBullets.Remove(disabledBullets[0]);
            StartCoroutine(DeactivateBullet(disabledBullet));
            Debug.Log("reuse bullet");
        }
        else
        {
            var instancedBullet = Instantiate(bullet, shootTransform.position, shootTransform.rotation, bulletPool);
            StartCoroutine(DeactivateBullet(instancedBullet));
            Debug.Log("make new bullet");
        }
    }

    IEnumerator DeactivateBullet(GameObject instancedBullet)
    {
        yield return new WaitForSeconds(destroyTime);
        
        instancedBullet.SetActive(false);
        disabledBullets.Add(instancedBullet);
        Debug.Log("add bullet to list");
    }
}
