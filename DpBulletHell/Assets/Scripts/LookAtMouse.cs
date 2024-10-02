using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject Player;
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - Player.transform.position.x, mousePosition.y - Player.transform.position.y);

        Player.transform.up = direction;
    }
}
