using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerInput playerInput;

    private void Awake()
    {
        instance = this;
    }


}
