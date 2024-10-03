using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeed : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb.AddForce(transform.up * 1000f);
    }
}
