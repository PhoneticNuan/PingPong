using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public string PlayerInput;
    public float MoveSpeed = 0f;

    public LayerMask invisibleLayer;

    private void Update()
    {
        float horizontal = Input.GetAxis(PlayerInput);
        if (horizontal > 0)
        {
            if (!Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y + 0.5f), invisibleLayer))
            {
                transform.Translate(Vector2.up * horizontal * MoveSpeed * Time.deltaTime);
            }
        }
        if (horizontal < 0)
        {
            if (!Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y - 0.5f), invisibleLayer))
            {
                transform.Translate(Vector2.up * horizontal * MoveSpeed * Time.deltaTime);
            }
        }
    }
}
