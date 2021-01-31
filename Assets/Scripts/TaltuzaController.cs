using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaltuzaController : MonoBehaviour
{

    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Hit");
            player.TakeDamage(1);
        }
    }
}
