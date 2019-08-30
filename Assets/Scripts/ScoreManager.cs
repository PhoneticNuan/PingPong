using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eccentric.Utils;

public class ScoreManager : TSingletonMonoBehavior<ScoreManager>
{
    public List<int> playerScore;

    [SerializeField]
    private int score;

    public void AddScore(int playerId)
    {
        playerScore[playerId] += score;
    }
}
