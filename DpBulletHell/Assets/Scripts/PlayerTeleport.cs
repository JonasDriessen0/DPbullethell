using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float teleportCooldown = 0.5f;
    [SerializeField] private LayerMask obstacleLayer;

    private bool canTeleport = true;
    private float lastTeleportTime;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && canTeleport)
        {
            TeleportToMousePosition();
        }
        
        if (!canTeleport && Time.time - lastTeleportTime >= teleportCooldown)
        {
            canTeleport = true;
        }
    }

    private void TeleportToMousePosition()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.Linecast(transform.position, mousePosition, obstacleLayer);
        if (hit.collider != null)
        {
            Debug.Log("Teleport destination is obstructed!");
            return;
        }

        transform.position = mousePosition;
        canTeleport = false;
        lastTeleportTime = Time.time;
        
    }
}