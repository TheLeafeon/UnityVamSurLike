using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("#Game Object")]
    public PlayerInput playerInput;
    public PoolingManager poolingManager;


    [Header("#Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10.0f;

    [Header("#Game Info")]
    public int level;
    public int killCount;
    public int exp;
    public int[] nextExp = { 3,5,10, 100, 150, 210, 280, 360, 450, 600 };



    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;

        }

    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
