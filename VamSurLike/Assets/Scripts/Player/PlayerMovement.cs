using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    PlayerInput input;
    SpriteRenderer spriter;
    Animator animator;

    public float Speed;
    public Scanner scanner;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();   
        spriter = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    private void FixedUpdate()
    {
        Vector2 nextVector = input.getInputVector.normalized * Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
    }

    void LateUpdate()
    {

        animator.SetFloat("Speed", input.getInputVector.magnitude);

        if(input.getInputVector.x != 0)
        {
            spriter.flipX = input.getInputVector.x < 0;
        }


    }
}
