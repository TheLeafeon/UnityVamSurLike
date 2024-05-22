using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    public float health;
    public  float maxHealth;
    public RuntimeAnimatorController[] animCon;
    Animator anim;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    
        anim =GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();   
        
    }

    private void OnEnable() //활성화될때 호출
    {
        target = GameManager.instance.playerInput.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }


    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];

        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void FixedUpdate()
    {
        if (isLive)
        {
            Vector2 directionVec = target.position - rigid.position;
            Vector2 nextVec = directionVec.normalized * speed * Time.fixedDeltaTime;

            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
    }

    private void LateUpdate()
    {
        if (isLive)
        {
            spriteRenderer.flipX = target.position.x < rigid.position.x;
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            health -= collision.GetComponent<Bullet>().damage;

            if(health > 0 )
            {
                // Live , Hit Action;
            }
            else
            {
                // die
                Dead();
            }
            
        }
        else
        return;
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
