using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public int remainingVegetables;

    //Atack Prefabs
    public GameObject GasPrefab;
    public GameObject SwordPrefab;
    public GameObject RockPrefab;
    public GameObject GasTankPrefab;

    public GameObject SunTrail;
    
    public SpriteRenderer gasTank;

    public List<Sprite> poisonTankSprites;

    public HealthBar healthbar;
    public GameObject healthbarObject;

    private UI_Manager _uiManager;

    private Rigidbody2D rb;

    private Animator _animator;

    [HideInInspector]
    public bool playingWithMouse;

    //Move with Input system
    [HideInInspector]
    public Vector2 movementDir;
    Vector3 lastMoveDir;

    //Move with mouse
    private Vector3 mousePosition;
    //private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeedMouse = 20f;

    int _currentHealth;

    bool _canAtack;
    bool _dash;

    private int _poisonTank;

    private void Start()
    {
        playingWithMouse = false;
        remainingVegetables = 10;
        _currentHealth = 10;
        healthbar.SetHealth(_currentHealth);
        healthbarObject.SetActive(false);

        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _dash = false;
        _canAtack = true;
        _poisonTank = 4;
        gasTank.enabled = false;
        Instantiate(GasTankPrefab, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
        Instantiate(SunTrail, transform.position + new Vector3(-0.52f, -0.57f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playingWithMouse)
        {
            //Input
            movementDir.x = Input.GetAxisRaw("Horizontal");
            movementDir.y = Input.GetAxisRaw("Vertical");

            _animator.SetFloat("Horizontal", movementDir.x);
            _animator.SetFloat("Vertical", movementDir.y);
            _animator.SetFloat("Speed", movementDir.sqrMagnitude);
        }

        Atack();
        Dash();

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
        }
        
    }

    private void FixedUpdate()
    {
        //Movement
        if (!playingWithMouse)
            Move();
        else
            MoveWithMouse();
        
        ManageDash();
    }

    void Move()
    {
        rb.MovePosition(rb.position + movementDir * moveSpeed * Time.fixedDeltaTime);
        lastMoveDir = movementDir;
    }

    void MoveWithMouse()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeedMouse, direction.y * moveSpeed);
        lastMoveDir = direction;
        _animator.SetFloat("Horizontal", direction.x);
        _animator.SetFloat("Vertical", direction.y);
        _animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    void Dash()
    {
        //Dash animation

        //Dash Logic
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetMouseButtonDown(1)) && !_dash)
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
            rb.AddForce(lastMoveDir * 3000f);
        }
    }

    void Atack()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetMouseButtonDown(0)) && _canAtack) //Input.GetAxisRaw("Atack") > 0
        {
            //AtackCooldown
            _canAtack = false;
            StartCoroutine(NextAtack());
            //Atack logic - Each atack has a diferent animation
            int AtackIndex = Random.Range(0, 3);

            Vector3 spawnPoint;
            float atackAngle;

            if (playingWithMouse)
                movementDir = direction;
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

                    //Poison stock logic
                    gasTank.enabled = true; //Gas tank sprite
                    StartCoroutine(HideGas());
                    if(_poisonTank > 0)
                    {
                        _poisonTank--;
                        gasTank.sprite = poisonTankSprites[_poisonTank];

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
                        GameObject atack2 = Instantiate(GasPrefab, spawnPoint, Quaternion.Euler(0, 0, atackAngle));
                    }
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

    public void CollectVegetable()
    {
        remainingVegetables--;
        //Update UI;
        _uiManager.UpdateRemainingVegetables(remainingVegetables);
    }

    public void TakeDamage(int damage)
    {
        //Show healthbar
        healthbarObject.SetActive(true);

        _currentHealth -= damage;
        healthbar.SetHealth(_currentHealth);

        if(_currentHealth <= 0)
        {
            //Die Logic
        }
        else
        {
            //Hide Healthbar
            StartCoroutine(HideHealthbar());
        }
    }

    IEnumerator NextAtack()
    {
        yield return new WaitForSeconds(0.5f);
        _canAtack = true;
    }

    IEnumerator HideGas()
    {
        yield return new WaitForSeconds(1.5f);
        gasTank.enabled = false;
    }

    IEnumerator HideHealthbar()
    {
        yield return new WaitForSeconds(2f);
        healthbarObject.SetActive(false);
    }

    IEnumerator CutDash()
    {
        yield return new WaitForSeconds(0.1f);
        _dash = false;
        rb.velocity = Vector2.zero;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "GasRefill")
        {
            _poisonTank = 4;
            gasTank.sprite = poisonTankSprites[3];
            gasTank.enabled = true;
            StartCoroutine(HideGas());
        }
    }
}
