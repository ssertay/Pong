using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [Header("Final Score UI")]
    [SerializeField] GameObject finalScoreText;
    [SerializeField] GameObject winnerText;

    void Awake()
    {
        int p1Score = StaticGameData.Instance.GetComponent<StaticGameData>().Player1Score;
        int p2Score = StaticGameData.Instance.GetComponent<StaticGameData>().Player2Score;

        string finalScoreStr = p1Score.ToString() + " - " + p2Score.ToString();

        finalScoreText.GetComponent<TextMeshProUGUI>().text = finalScoreStr;
        winnerText.GetComponent<TextMeshProUGUI>().text = p1Score > p2Score ? "P1 Wins" : "P2 Wins";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
