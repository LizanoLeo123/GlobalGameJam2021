using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    private Animator _animator;

    Vector2 movementDir;
    Vector3 lastMoveDir;

    bool _canAtack;
    bool _dash;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _dash = false;
        _canAtack = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movementDir.x = Input.GetAxisRaw("Horizontal");
        movementDir.y = Input.GetAxisRaw("Vertical");

        _animator.SetFloat("Horizontal", movementDir.x);
        _animator.SetFloat("Vertical", movementDir.y);
        _animator.SetFloat("Speed", movementDir.sqrMagnitude);

        //Debug.Log(movementDir.x + " , " + movementDir.y);

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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && _canAtack) //Input.GetAxisRaw("Atack") > 0
        {
            //AtackCooldown
            _canAtack = false;
            StartCoroutine(NextAtack());
            //Atack logic - Each atack has a diferent animation
            int AtackIndex = Random.Range(0, 3);
            Debug.Log(AtackIndex);

            //Logic to swith atacks
            switch (AtackIndex)
            {
                case 0:
                    _animator.SetTrigger("Machete");
                    //Instantiate Atack
                    break;
                case 1:
                    _animator.SetTrigger("Gas");
                    //Isntantiate Gas
                    break;
                case 2:
                    _animator.SetTrigger("Rock");
                    //Instatntiate Rock
                    break;
            }
            
        }
        
    }

    IEnumerator NextAtack()
    {
        yield return new WaitForSeconds(0.5f);
        _canAtack = true;
    }

    IEnumerator CutDash()
    {
        yield return new WaitForSeconds(0.1f);
        _dash = false;
        rb.velocity = Vector2.zero;
    }
}
