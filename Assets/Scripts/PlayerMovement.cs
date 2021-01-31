using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    //Atack Prefabs
    public GameObject GasPrefab;
    public GameObject SwordPrefab;
    public GameObject RockPrefab;

    private Rigidbody2D rb;

    private Animator _animator;

    [HideInInspector]
    public Vector2 movementDir;
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
            //Debug.Log(AtackIndex);


            Vector3 spawnPoint;
            float atackAngle;

            //Logic to swith atacks
            switch (AtackIndex)
            {
                case 0:
                    _animator.SetTrigger("Machete");

                    if (Mathf.Abs(movementDir.x) > Mathf.Abs(movementDir.y)) //Facing left or right
                    {
                        if (movementDir.x < 0) //Facing left
                        {
                            atackAngle = 270;
                            spawnPoint = transform.position + new Vector3(-1.2f, 0, 0);
                        }
                        else //Facing right
                        {
                            atackAngle = 90;
                            spawnPoint = transform.position + new Vector3(1.2f, 0, 0);
                        }
                    }
                    else //Facing Up or down
                    {
                        if (movementDir.y > 0) //Facing Up
                        {
                            atackAngle = 180;
                            spawnPoint = transform.position + new Vector3(0, 1.2f, 0);
                        }
                        else //Facing Down
                        {
                            atackAngle = 0;
                            spawnPoint = transform.position + new Vector3(0, -1.2f, 0);
                        }
                    }
                    //Instantiate Atack
                    GameObject atack = Instantiate(SwordPrefab, spawnPoint, Quaternion.Euler(0,0, atackAngle));

                    break;
                case 1:
                    _animator.SetTrigger("Gas");

                    if (Mathf.Abs(movementDir.x) > Mathf.Abs(movementDir.y)) //Facing left or right
                    {
                        if (movementDir.x < 0) //Facing left
                        {
                            atackAngle = 0;
                            spawnPoint = transform.position + new Vector3(-2.5f, 0, 0);
                        }
                        else //Facing right
                        {
                            atackAngle = 180;
                            spawnPoint = transform.position + new Vector3(2.5f, 0, 0);
                        }
                    }
                    else //Facing Up or down
                    {
                        if (movementDir.y > 0) //Facing Up
                        {
                            atackAngle = 270;
                            spawnPoint = transform.position + new Vector3(0, 2.5f, 0);
                        }
                        else //Facing Down
                        {
                            atackAngle = 90;
                            spawnPoint = transform.position + new Vector3(0, -2.5f, 0);
                        }
                    }
                    //Instantiate Gas
                    GameObject atack2 = Instantiate(GasPrefab, spawnPoint, Quaternion.Euler(0,0, atackAngle));

                    break;
                case 2:
                    _animator.SetTrigger("Rock");
                    //Instatntiate Rock

                    if (Mathf.Abs(movementDir.x) > Mathf.Abs(movementDir.y)) //Facing left or right
                    {
                        if (movementDir.x < 0) //Facing left
                        {
                            atackAngle = 270;
                            spawnPoint = transform.position + new Vector3(-0.5f, 0, 0);
                        }
                        else //Facing right
                        {
                            atackAngle = 90;
                            spawnPoint = transform.position + new Vector3(0.5f, 0, 0);
                        }
                    }
                    else //Facing Up or down
                    {
                        if (movementDir.y > 0) //Facing Up
                        {
                            atackAngle = 180;
                            spawnPoint = transform.position + new Vector3(0, 0.5f, 0);
                        }
                        else //Facing Down
                        {
                            atackAngle = 0;
                            spawnPoint = transform.position + new Vector3(0, -0.5f, 0);
                        }
                    }
                    //Instantiate Atack
                    GameObject atack3 = Instantiate(RockPrefab, spawnPoint, Quaternion.Euler(0, 0, atackAngle));
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
