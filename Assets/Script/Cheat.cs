using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    private SelectManager _SelectorManager;

    private void Start()
    {
        _SelectorManager = GetComponent<SelectManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _SelectorManager.levelName = "LevelOne";
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            _SelectorManager.levelName = "LevelTwo";
        }
    }
}
