using System;
using System.Diagnostics;
using UnityEngine.SocialPlatforms.Impl;

namespace HighScores
{
    // Serializable class representing a collection of high scores
    [Serializable]
    public class HighScores
    {
        // Array to store individual high scores
        public HighScore[] scores;
    }

    // Serializable class representing an individual high score
    [Serializable]
    public class HighScore
    {
        // Unique identifier for the high score
        public int id;

        // Player's name associated with the high score
        public string playername = "";

        // Numeric score achieved by the player
        public float score = 0.0f;
       
        // Date and time when the high score was achieved
        public string playtime = "";
    }
}

