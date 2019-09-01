using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class SelectManager : MonoBehaviour {
    [SerializeField] string player1Input;
    [SerializeField] string player2Input;
    [SerializeField] string player1Certain;
    [SerializeField] string player2Certain;
    [SerializeField] List<Texture2D> playerTexs;
    [SerializeField]List<string> playerNames;
    [SerializeField] RawImage player1Image;
    [SerializeField] RawImage player2Image;
    [SerializeField] Text player1Name;
    [SerializeField] Text player2Name;
    [Header ("Monitoring")]
    [SerializeField] int player1Chosen = 0;
    [SerializeField] int player2Chosen = 0;
    bool bPlayer1Lock;
    bool bPlayer2Lock;

    public string levelName;

    void Start ( ) {
        bPlayer1Lock = false;
        bPlayer2Lock = false;
    }

    void Update ( ) {
        if (!bPlayer1Lock && Input.GetButtonDown (player1Input)) {
            float tmp = Input.GetAxisRaw (player1Input);
            player1Chosen = CheckRange (player1Chosen + (int) tmp);
        }

        if (!bPlayer2Lock && Input.GetButtonDown (player2Input)) {
            float tmp = Input.GetAxisRaw (player2Input);
            player2Chosen = CheckRange (player2Chosen + (int) tmp);
        }

        if (Input.GetButtonDown (player1Certain))
            bPlayer1Lock = true;
        if (Input.GetButtonDown (player2Certain))
            bPlayer2Lock = true;
        Check ( );
    }

    int CheckRange (int current) {
        int result = current;
        int max = playerTexs.Count - 1;
        int min = 0;
        if (result < min)
            result = max;
        else if (result > max)
            result = 0;
        return result;
    }

    void Check ( ) {
        UpdateUI();
        if (bPlayer1Lock && bPlayer2Lock) {
            GameManager.Instance.SelectPlayer (player1Chosen, player2Chosen);
            GameManager.Instance.LoadScene(levelName);
        }

    }

    void UpdateUI ( ) {
        player1Name.text=playerNames[player1Chosen];
        player2Name.text=playerNames[player2Chosen];
        player1Image.texture=playerTexs[player1Chosen];
        player2Image.texture=playerTexs[player2Chosen];
    }
}
