using System.Collections.Generic;

using Eccentric.Utils;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : TSingletonMonoBehavior<GameManager>
{
    public string SceneName;

    public List<int> playerScore;
    [SerializeField]
    private int score;
    [SerializeField]
    private int playerHitScore;
    [SerializeField]
    private Text player1Text;
    [SerializeField]
    private Text player2Text;
    private bool gamePlayStart = false;
    
    [SerializeField] List<GameObject> players;
    public int player1;
    public int player2;

    public void PlayerSpawn(Transform p1tf, Transform p2tf)
    {
        GameObject p1 = GameObject.Instantiate(players[player1]);
        GameObject p2 = GameObject.Instantiate(players[player2]);
        p1.transform.position = p1tf.position;
        p2.transform.position = p2tf.position;
        p1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        p2.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        PlayerShooting p1Shoot = p1.GetComponent<PlayerShooting>();
        PlayerShooting p2Shoot = p2.GetComponent<PlayerShooting>();
        p1Shoot.rotateStartValue = -50f;
        p2Shoot.rotateStartValue = -230f;
        p1Shoot.shootInput = "Fire1";
        p2Shoot.shootInput = "Fire2";
        CharacterMovement p1Move = p1.GetComponent<CharacterMovement>();
        CharacterMovement p2Move = p2.GetComponent<CharacterMovement>();
        p1Move.PlayerInput = "PlayerOneMovement";
        p2Move.PlayerInput = "PlayerTwoMovement";
        p1.name = "Player1";
        p2.name = "Player2";
        p1.tag = "Player1";
        p2.tag = "Player2";

        p1.GetComponent<PlayerShooting>().rotateStartValue = -70f;
        p2.GetComponent<PlayerShooting>().rotateStartValue = -250f;
    }
    public void SelectPlayer(int player1, int player2)
    {
        this.player1 = player1;
        this.player2 = player2;
        Debug.Log(player1);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            if (Input.anyKeyDown)
            {
                LoadScene(SceneName);
            }
        }

        if (SceneManager.GetActiveScene().name == "LevelOne" || SceneManager.GetActiveScene().name == "LevelTwo")
        {
            if (!gamePlayStart)
            {
                gamePlayStart = true;
                GamePlayInit();
            }
        }
        else
        {
            gamePlayStart = false;
        }


    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void GamePlayInit()
    {
        player1Text = GameObject.Find("Player1Score").GetComponent<Text>();
        player2Text = GameObject.Find("Player2Score").GetComponent<Text>();
        playerScore = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            playerScore.Add(0);
        }
    }

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