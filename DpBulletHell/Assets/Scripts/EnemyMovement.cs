using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float strafeDuration = 1f;
    [SerializeField] private float strafeInterval = 3f;
    [SerializeField] private Rigidbody2D rb;

    private Transform player;
    private Vector2 movement;
    private bool isStrafing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StrafeBehavior());
    }

    private void Update()
    {
        if (!isStrafing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction * moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private IEnumerator StrafeBehavior()
    {
        while (true)
        {
            yield return new WaitForSeconds(strafeInterval);
            
            if (Random.value > 0.5f)
            {
                isStrafing = true;
                Vector2 perpendicularDirection = Vector2.Perpendicular((player.position - transform.position).normalized);
                movement = perpendicularDirection * moveSpeed * (Random.value > 0.5f ? 1 : -1);

                yield return new WaitForSeconds(strafeDuration);

                isStrafing = false;
            }
        }
    }
}