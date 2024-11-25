using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class JsonReadWriteSystem : MonoBehaviour
{
    public TextMeshProUGUI Player1ScoreText;
    public TextMeshProUGUI Player2ScoreText;
    public TextMeshProUGUI LeaderboardText;

    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/HighScores.json";

        //// Check if we're in the leaderboard scene
        //if (LeaderboardText != null)
        //{
        //    LoadHighestScore();
        //}
    }

    public void SaveToJson()
    {
        int player1Score = int.Parse(Player1ScoreText.text);
        int player2Score = int.Parse(Player2ScoreText.text);

        // Load existing scores from the file
        List<MatchData> matchHistory = LoadMatchHistory();

        // Add the new match result
        matchHistory.Add(new MatchData
        {
            Player1Score = player1Score,
            Player2Score = player2Score
        });

        // Keep only the last 5 matches
        if (matchHistory.Count > 5)
        {
            matchHistory.RemoveAt(0);
        }

        // Save the updated history to JSON
        string json = JsonUtility.ToJson(new MatchHistory { Matches = matchHistory }, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Scores saved to JSON.");
    }

    public void LoadHighestScore()
    {
        List<MatchData> matchHistory = LoadMatchHistory();

        // Find the highest score and corresponding player
        int highestScore = 0;
        string highestScoringPlayer = "No Data";

        foreach (MatchData match in matchHistory)
        {
            if (match.Player1Score > highestScore)
            {
                highestScore = match.Player1Score;
                highestScoringPlayer = $"Player 1: {highestScore}";
            }
            if (match.Player2Score > highestScore)
            {
                highestScore = match.Player2Score;
                highestScoringPlayer = $"Player 2: {highestScore}";
            }
        }

        // Display the highest score
        if (LeaderboardText != null)
        {
            LeaderboardText.text = $"Highest Score:\n{highestScoringPlayer}";
        }
    }

    private List<MatchData> LoadMatchHistory()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            MatchHistory matchHistory = JsonUtility.FromJson<MatchHistory>(json);
            return matchHistory.Matches;
        }

        return new List<MatchData>();
    }

   public void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
}

[System.Serializable]
public class MatchData
{
    public int Player1Score;
    public int Player2Score;
}

[System.Serializable]
public class MatchHistory
{
    public List<MatchData> Matches = new List<MatchData>();
}
