using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootTransform;
    [SerializeField] private GameObject bullet;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, shootTransform.position, shootTransform.rotation);
    }
}
