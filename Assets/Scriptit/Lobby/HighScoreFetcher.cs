using UnityEngine;

public class HighScoreFetcher : BackendHandler
{
    // Override the Start method to only fetch high scores
    void Start()
    {
        Debug.Log("HighScoreFetcher started");
        FetchhighScoresJSON();
    }
}
