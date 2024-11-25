using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // For loading scenes
using System.IO;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject player1Paddle;
    public GameObject player1Goal;

    [Header("Player 2")]
    public GameObject player2Paddle;
    public GameObject player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;

    [Header("Timer UI")]
    public TextMeshProUGUI timerText;

    private int Player1Score;
    private int Player2Score;

    private float gameDuration = 60f; // Game duration in seconds
    private float timer;

    private string filePath;

    private void Start()
    {
        timer = gameDuration;
        filePath = Application.dataPath + "/PlayerDataFile.json";
    }

    private void Update()
    {
        // Update timer
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    public void Player1Scored()
    {
        Player1Score++;
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
        ResetPosition();
    }

    public void Player2Scored()
    {
        Player2Score++;
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
        ResetPosition();
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        player1Paddle.GetComponent<Paddle>().Reset();
        player2Paddle.GetComponent<Paddle>().Reset();
    }

    private void EndGame()
    {
        // Arrange the scores in descending order
        DisplayHighScoredPlayer();

        // Save the scores to JSON
        SaveScores();

        // Load the score scene
        SceneManager.LoadScene("ScoreScene");
    }


    private void SaveScores()
    {
        PlayerData data = new PlayerData
        {
            Player1 = Player1Score.ToString(),
            Player2 = Player2Score.ToString()
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Game data saved to JSON.");
    }

    private void DisplayHighScoredPlayer()
    {
        TextMeshProUGUI topText = Player1Text.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bottomText = Player2Text.GetComponent<TextMeshProUGUI>();

        // Compare scores and arrange
        if (Player1Score > Player2Score)
        {
            topText.text = $"Player 1: {Player1Score}";
            bottomText.text = $"Player 2: {Player2Score}";
        }
        else if (Player2Score > Player1Score)
        {
            topText.text = $"Player 2: {Player2Score}";
            bottomText.text = $"Player 1: {Player1Score}";
        }
        else
        {
            // Scores are equal
            topText.text = $"Player 1: {Player1Score}";
            bottomText.text = $"Player 2: {Player2Score}";
        }
    }



}



[System.Serializable]
public class PlayerData
{
    public string Player1;
    public string Player2;
}
