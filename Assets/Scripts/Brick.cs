using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private bool breakable = true;
    [SerializeField]
    private int health = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (breakable)
        {
            health -= 1;

            if (health == 0)
            {
                if (collision.gameObject.tag == "Player1Ball")
                {
                    ScoreManager.Instance.AddScore(0, 1);
                    Destroy(gameObject);
                }
                else if (collision.gameObject.tag == "Player2Ball")
                {
                    ScoreManager.Instance.AddScore(1, 1);
                    Destroy(gameObject);
                }
            }
        }
    }
}
