using UnityEngine;
using TMPro;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI WinnerText;
    public TextMeshProUGUI Player1ScoreText;
    public TextMeshProUGUI Player2ScoreText;

    private string filePath;

    private void Start()
    {
        // Define the path to the JSON file
        filePath = Application.dataPath + "/PlayerDataFile.json";

        // Load and display the high score
        LoadScoresAndDisplayWinner();
    }

    private void LoadScoresAndDisplayWinner()
    {
        if (File.Exists(filePath))
        {
            // Read the JSON file
            string json = File.ReadAllText(filePath);

            // Deserialize JSON into PlayerData
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Parse scores and determine the winner
            int player1Score = int.Parse(data.Player1);
            int player2Score = int.Parse(data.Player2);

            Player1ScoreText.text = $"Player 1 Score: {player1Score}";
            Player2ScoreText.text = $"Player 2 Score: {player2Score}";

            // Determine and display the winner
            if (player1Score > player2Score)
            {
                WinnerText.text = $"[WINNER] Player 1 Wins with {player1Score} Points!";
            }
            else if (player2Score > player1Score)
            {
                WinnerText.text = $"[WINNER] Player 2 Wins with {player2Score} Points!";
            }
            else
            {
                WinnerText.text = "[TIE] Both Players Scored Equally!";
            }

            Debug.Log("High scores and winner loaded successfully.");
        }
        else
        {
            Debug.LogError("Score file not found at: " + filePath);
            Player1ScoreText.text = "Player 1 Score: 0";
            Player2ScoreText.text = "Player 2 Score: 0";
            WinnerText.text = "No scores available.";
        }
    }
}
