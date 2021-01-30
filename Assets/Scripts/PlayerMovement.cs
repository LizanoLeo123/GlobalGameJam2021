﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    Vector2 movementDir;
    Vector3 lastMoveDir;

    bool _dash;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _dash = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movementDir.x = Input.GetAxisRaw("Horizontal");
        movementDir.y = Input.GetAxisRaw("Vertical");

        Atack();
        Dash();
        
    }

    private void FixedUpdate()
    {
        //Movement
        Move();
        ManageDash();
    }

    void Move()
    {
        rb.MovePosition(rb.position + movementDir * moveSpeed * Time.fixedDeltaTime);
        lastMoveDir = movementDir;
    }

    void Dash()
    {
        //Dash animation

        //Dash Logic
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && !_dash)
        {
            _dash = true;
        }
    }

    void ManageDash()
    {
        if (_dash)
        {
            //Start cooldown
            StartCoroutine(CutDash());
            rb.AddForce(lastMoveDir * 4000f);
        }
    }

    void Atack()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) //Input.GetAxisRaw("Atack") > 0
        {
            //Atack logic - Each atack has a diferent animation
            int AtackIndex = Random.Range(0, 5);
            Debug.Log(AtackIndex);
        }
    }

    IEnumerator CutDash()
    {
        yield return new WaitForSeconds(0.1f);
        _dash = false;
        rb.velocity = Vector2.zero;
    }
}
