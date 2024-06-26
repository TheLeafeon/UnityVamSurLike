using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //pooling Manager 에서 받아온 무기의 관리를 위한 코드

    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;

    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>(); 
    }

    private void Start()
    {
        Init();
       
    }





    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150;
                Batch();

                break;
            default:

                
                speed = 0.3f;
                break;
        }
    }

    void Batch()
    {
        for(int index = 0; index < count; index ++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index); 
            }
            else
            {
                bullet = GameManager.instance.poolingManager.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;

            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);// -1 is Infinity Per.

        }
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                timer += Time.deltaTime;
                
                if (timer > speed) 
                {
                    timer = 0.0f;
                    Fire();
                }
                break;
        }

        // 테스트 코드
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }


    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count = count;

        if(id == 0)
        {
            Batch();
        }
    }

    private void Fire()
    {
        if(!playerMovement.scanner.nearestTarget)
            return;

        Vector3 targetPos = playerMovement.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.poolingManager.Get(prefabId).transform;
        bullet.position = transform.position;

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        bullet.GetComponent<Bullet>().Init(damage, count, dir);

    }
}
