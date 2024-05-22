using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public PlayerInput playerInput;
    public PoolingManager poolingManager;

    public float gameTime;
    public float maxGameTime  = 2 * 10.0f;


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

}
