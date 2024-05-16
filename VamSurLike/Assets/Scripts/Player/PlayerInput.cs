using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    private Vector2 inputVector;

    public Vector2  getInputVector
    {
        get { return inputVector; }

    }

    private void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }
}
