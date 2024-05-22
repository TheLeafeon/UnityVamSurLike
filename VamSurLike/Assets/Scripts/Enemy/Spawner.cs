using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public SpawnData[] spawnData;

    float timer;
    int level;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10.0f), spawnData.Length - 1) ;

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0.0f;
            Spawn();
        }
    }

    void Spawn()
    {
       GameObject enemy = GameManager.instance.poolingManager.Get(0);

       enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;

        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

//Á÷·ÄÈ­
[System.Serializable]
public class SpawnData
{
    public float spawnTime;

    public int spriteType;
    public int health;
    public float speed;
}