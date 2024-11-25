//public void LoadHighestScore()
//{
//    List<MatchData> matchHistory = LoadMatchHistory();

//    // Find the highest score and corresponding player
//    int highestScore = 0;
//    string highestScoringPlayer = "No Data";

//    foreach (MatchData match in matchHistory)
//    {
//        if (match.Player1Score > highestScore)
//        {
//            highestScore = match.Player1Score;
//            highestScoringPlayer = $"Player 1: {highestScore}";
//        }
//        if (match.Player2Score > highestScore)
//        {
//            highestScore = match.Player2Score;
//            highestScoringPlayer = $"Player 2: {highestScore}";
//        }
//    }

//    // Display the highest score
//    if (LeaderboardText != null)
//    {
//        LeaderboardText.text = $"Highest Score:\n{highestScoringPlayer}";
//    }
//}