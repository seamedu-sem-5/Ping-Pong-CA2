using UnityEngine;
using TMPro;
using System.IO;

public class ScoreLoader : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI Player1ScoreText;
    public TextMeshProUGUI Player2ScoreText;

    private string filePath;

    private void Start()
    {
        // Define the path to the JSON file
        filePath = Application.dataPath + "/PlayerDataFile.json";

        // Load and display the scores
        LoadScores();
    }

    private void LoadScores()
    {
        if (File.Exists(filePath))
        {
            // Read the JSON file
            string json = File.ReadAllText(filePath);

            // Deserialize JSON into PlayerData
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Update the UI with the scores
            Player1ScoreText.text = $"Player 1 Score: {data.Player1}";
            Player2ScoreText.text = $"Player 2 Score: {data.Player2}";

            Debug.Log("Scores loaded successfully.");
        }
        else
        {
            Debug.LogError("Score file not found at: " + filePath);
            Player1ScoreText.text = "Player 1 Score: 0";
            Player2ScoreText.text = "Player 2 Score: 0";
        }
    }
}
