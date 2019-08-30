using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private bool breakable;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (breakable)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                Destroy(gameObject);
            }
        }
    }
}
