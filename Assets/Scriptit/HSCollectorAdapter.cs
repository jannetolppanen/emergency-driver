using System;
using HighScores;

public class HSCollectorAdapter
{
    private HighScores.HighScore _highScore;

    public HSCollectorAdapter(HighScores.HighScore highScore)
    {
        _highScore = highScore;
    }

    public void UpdateTimeAndName(string time, string name)
    {
        // Update the time and name in the HighScore object
        _highScore.playtime = time;
        _highScore.score = float.TryParse(time, out var parsedTime) ? parsedTime : 0.0f;
        _highScore.playername = name;
    }
}
