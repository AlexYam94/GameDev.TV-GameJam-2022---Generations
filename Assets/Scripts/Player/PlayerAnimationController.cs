using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    [SerializeField] Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(bool jump)
    {
        _anim.SetBool("jump", jump);
    }

    public void Move(float value)
    {
        _anim.SetFloat("move", value);
    }

    public void Attack()
    {
        _anim.SetTrigger("attack");
    }

    public void Death()
    {
        _anim.SetTrigger("death");
    }
}
