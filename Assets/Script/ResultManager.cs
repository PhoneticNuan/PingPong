using Eccentric.Utils;

using UnityEngine;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour
{
    [SerializeField] float gameTime;
    [SerializeField] Text reaminTimeText;
    CountdownTimer timer;
    void Start()
    {
        timer = new CountdownTimer(gameTime);
        reaminTimeText.text = timer.Remain.ToString();
    }
    void Update()
    {
        int timeRemain = (int)timer.Remain;
        reaminTimeText.text = timeRemain.ToString();
        if (timer.IsFinished)
            GameFin();

    }
    void GameFin()
    {
        string winner = "";
        if (ScoreManager.Instance.playerScore[0] >= ScoreManager.Instance.playerScore[1])
            winner = "Player1";
        else
            winner = "Player2";
        Debug.Log("Gamefin winner goes to " + winner);
    }
}
