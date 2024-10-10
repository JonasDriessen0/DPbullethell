using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 10f;
    public int damage = 10;

    public void Shoot(Vector2 direction)
    {
        rb.velocity = direction * bulletSpeed;
    }
    
    public void ResetBullet()
    {
        rb.velocity = Vector2.zero;
    }
}