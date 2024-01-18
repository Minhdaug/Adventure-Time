using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask solidObjectLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Normalize the movement vector to ensure constant speed regardless of direction
        movement.Normalize();

        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        // Calculate the new position
        Vector2 newPosition = (Vector2)transform.position + (direction * moveSpeed * Time.deltaTime);

        // Perform a raycast to check for collisions with solid objects
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, moveSpeed * Time.deltaTime, solidObjectLayer);

        // If there is no collision, move the character
        if (hit.collider == null)
        {
            rb.MovePosition(newPosition);
        }
    }
}
