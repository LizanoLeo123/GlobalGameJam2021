using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBehavior : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D _collider;

    private TaltuzaController taltuza;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Fire");
        _collider = GetComponent<BoxCollider2D>();
        Destroy(gameObject, 1f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            taltuza = collision.transform.GetComponent<TaltuzaController>();
            taltuza.GetComponent<Animator>().SetBool("isPoisoned", true);
            //Logic To damage Enemy
            //Debug.Log("GasHit");
            StartCoroutine(PoisonedTime());
            _collider.enabled = false;
        }
    }

    IEnumerator PoisonedTime()
    {
        yield return new WaitForSeconds(0.9f);
        taltuza.GetComponent<Animator>().SetBool("isPoisoned", false);
    }
}
