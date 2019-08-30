using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public string PlayerInput;
    public float MoveSpeed = 0f;

    private void Update()
    {
        float horizontal = Input.GetAxis(PlayerInput);
        transform.Translate(Vector2.up * horizontal * MoveSpeed * Time.deltaTime);
    }
}
