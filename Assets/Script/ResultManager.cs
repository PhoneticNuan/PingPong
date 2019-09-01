using Eccentric.Utils;
using Eccentric.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ResultManager : MonoBehaviour
{
    [SerializeField] float gameTime;
    [SerializeField] Text reaminTimeText;
    CountdownTimer timer;

    [SerializeField] Transform p1SpawnPos;
    [SerializeField] Transform p2SpawnPos;

    public RawImage winImage;
    public RawImage winTextImage;
    public Texture2D[] winTextures;
    public Texture2D[] winTextTextures;

    private float EndTimer = 5f;

    private bool GameFinish = false;
    void Awake()
    {
        foreach (ObjectPool item in PaintballPool.Instance.balls)
        {
            item.Init();
        }
    }

    void Start()
    {
        timer = new CountdownTimer(gameTime);
        reaminTimeText.text = timer.Remain.ToString();

        GameManager.Instance.PlayerSpawn(p1SpawnPos, p2SpawnPos);

    }
    void Update()
    {
        int timeRemain = (int)timer.Remain;
        reaminTimeText.text = timeRemain.ToString();
        if (timer.IsFinished)
            GameFin();

        if (GameFinish)
        {
            EndTimer -= Time.deltaTime;

            if (EndTimer <= 0f)
            {
                if (Input.anyKeyDown)
                    GameManager.Instance.LoadScene("CharacterSelect");
            }
        }
    }
    void GameFin()
    {
        if (!GameFinish)
        {
            string winner = "";

            if (GameManager.Instance.playerScore[0] >= GameManager.Instance.playerScore[1])
            {
                winner = "Player1";
                Debug.Log(GameManager.Instance.player1);
                winImage.texture = winTextures[GameManager.Instance.player1];
                winTextImage.texture = winTextTextures[0];
            }
            else
            {
                winner = "Player2";
                Debug.Log(GameManager.Instance.player2);
                winImage.texture = winTextures[GameManager.Instance.player2];
                winTextImage.texture = winTextTextures[1];
            }
            Debug.Log("Gamefin winner goes to " + winner);

            winImage.gameObject.SetActive(true);

            EndTimer = 5f;

            GameFinish = true;
        }

    }
}
