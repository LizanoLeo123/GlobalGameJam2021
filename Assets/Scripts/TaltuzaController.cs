using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaltuzaController : MonoBehaviour
{
    public int patrolNumber;
    private PlayerMovement player;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && _animator.GetBool("isFollowing") == true)
        {
            Debug.Log("Hit");
            player.TakeDamage(1);
        }
    }

    private void Poisoned()
    {
        _animator.SetBool("isPoisoned", true);
        StartCoroutine(PoisonedTime());
    }

    IEnumerator PoisonedTime()
    {
        Debug.Log("Hola");
        yield return new WaitForSeconds(0.9f);
        Debug.Log("Hola2");
        _animator.GetComponent<Animator>().SetBool("isPoisoned", false);
        Debug.Log("Hola3");
    }
}
