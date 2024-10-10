using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BulletPool : MonoBehaviour
{
    public static BulletPool instance { get; private set; }
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialPoolSize = 100;
    private List<GameObject> pooledBullets = new List<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
        InitializePool();
    }
    
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            pooledBullets.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in pooledBullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        
        GameObject newBullet = Instantiate(bulletPrefab, transform);
        pooledBullets.Add(newBullet);
        return newBullet;
    }
    
    public void ReturnBullet(GameObject bullet)
    {
        bullet.GetComponent<Bullet>()?.ResetBullet();
        bullet.SetActive(false);
    }
}
