using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaltuzaAnimationController : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody2D _rigidbody2d;

    [HideInInspector]

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("VelX", _rigidbody2d.velocity.normalized.x);
        _animator.SetFloat("VelY", _rigidbody2d.velocity.normalized.y);
    }

}
