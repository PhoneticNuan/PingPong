using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    private float health;

    public void GetHurt(float damage)
    {
        health -= damage;
    }
}
