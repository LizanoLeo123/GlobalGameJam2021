using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    public CircleCollider2D _collider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();

        //Logic to throw Rock
        int angle = Random.Range(-15, 15);
        transform.rotation = Quaternion.Euler(0, 0,transform.rotation.eulerAngles.z + angle);

        Vector2 direction = GameObject.Find("Player").GetComponent<PlayerMovement>().movementDir;        
        rb.velocity = direction * 15f;
        Destroy(gameObject, 2.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            //Logic To damage Enemy
            //Debug.Log("RockHit");
            _collider.enabled = false;
            Destroy(gameObject, 0.1f);
        }
    }
}
