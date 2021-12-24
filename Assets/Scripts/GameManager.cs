using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    GameObject ball;

    [SerializeField] GameObject paddlePrefab;
    GameObject player1Paddle;
    GameObject player2Paddle;

    [Header("Score UI")]
    [SerializeField] GameObject player1Text;
    [SerializeField] GameObject player2Text;

    void Awake()
    {
        if (ball == null)
        {
            ball = Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

        if (player1Paddle == null)
        {
            player1Paddle = Instantiate(paddlePrefab, new Vector3(-8, 0, 0), Quaternion.identity);
            player1Paddle.tag = "PaddleLeft";
        }

        if (player2Paddle == null)
        {
            player2Paddle = Instantiate(paddlePrefab, new Vector3(8, 0, 0), Quaternion.identity);
            player2Paddle.tag = "PaddleRight";
        }

        resetScore();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    bool gameOver()
    {
        if (StaticGameData.Instance.GetComponent<StaticGameData>().Player2Score == 10
            || StaticGameData.Instance.GetComponent<StaticGameData>().Player1Score == 10)
        {
            return true;
        }

        return false;
    }

    IEnumerator WaitAndLoadGameOverScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("GameOver");
    }

    void resetBallAndPaddles()
    {
        player1Paddle.GetComponent<Paddle>().Reset();
        player2Paddle.GetComponent<Paddle>().Reset();
        ball.GetComponent<Ball>().Reset();
    }

    void resetScore()
    {
        StaticGameData.Instance.GetComponent<StaticGameData>().Player1Score = 0;
        StaticGameData.Instance.GetComponent<StaticGameData>().Player2Score = 0;
        updateScoreboard();
    }

    void updateScoreboard()
    {
        player1Text.GetComponent<TextMeshProUGUI>().text = StaticGameData.Instance.GetComponent<StaticGameData>().Player1Score.ToString();
        player2Text.GetComponent<TextMeshProUGUI>().text = StaticGameData.Instance.GetComponent<StaticGameData>().Player2Score.ToString();
    }

    public void playerScored(string tag)
    {
        if (tag == "LeftGoal")
        {
            StaticGameData.Instance.GetComponent<StaticGameData>().Player2Score++;
        }

        else if (tag == "RightGoal")
        {
            StaticGameData.Instance.GetComponent<StaticGameData>().Player1Score++;
        }

        updateScoreboard();

        if (gameOver())
        {
            IEnumerator coroutine = WaitAndLoadGameOverScene(1f);
            StartCoroutine(coroutine);
            return;
        }

        resetBallAndPaddles();
    }
}
