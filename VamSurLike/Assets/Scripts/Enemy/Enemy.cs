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
    public Collider2D coll;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    
        coll = GetComponent<Collider2D>();  
        anim =GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();   
        wait = new WaitForFixedUpdate();
    }

    private void OnEnable() //활성화될때 호출
    {
        target = GameManager.instance.playerInput.GetComponent<Rigidbody2D>();
        isLive = true;

        coll.enabled = true;
        rigid.simulated = true;
        spriteRenderer.sortingOrder = 2;
        anim.SetBool("Dead", false);

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
        if (!isLive ||  anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

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
        if (!collision.CompareTag("Bullet") || !isLive)
            return;


            health -= collision.GetComponent<Bullet>().damage;

            if(health > 0 )
            {  
                anim.SetTrigger("Hit");

                StartCoroutine(KnockBack());
                // Live , Hit Action;
            }
            else
            {
                // die
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriteRenderer.sortingOrder = 1;

                anim.SetBool("Dead", true);
                GameManager.instance.killCount++;
                GameManager.instance.GetExp();
            }

    }

    IEnumerator KnockBack()
    {



        yield return wait;

        Vector3 playerPos = GameManager.instance.playerInput.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        rigid.AddForce(dirVec.normalized * 10 , ForceMode2D.Impulse); //3은 밀리는 정도

    }


    void Dead()
    {
        gameObject.SetActive(false);
    }
}
