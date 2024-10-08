using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 1000f;

    private void Start()
    {
        rb.AddForce(transform.up * bulletSpeed);
    }
}
