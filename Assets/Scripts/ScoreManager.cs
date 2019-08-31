using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eccentric.Utils;
using UnityEngine.UI;

public class ScoreManager : TSingletonMonoBehavior<ScoreManager>
{
    public List<int> playerScore;

    [SerializeField]
    private int score;
    [SerializeField]
    private int playerHitScore;

    [SerializeField]
    private Text player1Text;
    [SerializeField]
    private Text player2Text;

    public void AddScore(int playerId, int scoreType = 0)
    {
        if (scoreType == 0)
            playerScore[playerId] += playerHitScore;
        else
            playerScore[playerId] += score;

        player1Text.text = playerScore[0].ToString();
        player2Text.text = playerScore[1].ToString();
    }
}
