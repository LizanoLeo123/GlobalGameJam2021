using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    Vector2 movementDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movementDir.x = Input.GetAxisRaw("Horizontal");
        movementDir.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        //Movement
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + movementDir * moveSpeed * Time.fixedDeltaTime);
    }

    void Dash()
    {

    }

    void Atack()
    {

    }
}
