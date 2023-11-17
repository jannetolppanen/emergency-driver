using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class BackendHandler : MonoBehaviour
{
 
 
  
    const string urlBackendHighScores = "https://niisku.lab.fi/~x108669/phpdemo/api/v1/highscores.php";

   
    
    // HighScores object to store the fetched high scores
    HighScores.HighScores hs;

    // Logging info
    string log = "";
    int fetchCounter = 0;
   
    // UI elements
    public UnityEngine.UI.Text loggingText;
    
    public UnityEngine.UI.Text highScoresTextArea;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BackendHandler started");
        PostGameResults();
        FetchhighScoresJSON();

       
    }

    bool updateHighScoreTextArea = false;

    // Update is called once per frame
    void Update()
    {
        loggingText.text = log;

        if (updateHighScoreTextArea)
        {
            // Update the UI with the high scores
            highScoresTextArea.text = CreateHighScoreList();
            updateHighScoreTextArea = false;
        }
    }

  

    // Method to fetch high scores from the backend
    public void FetchhighScoresJSON()
    {
        fetchCounter++;
        
        StartCoroutine(GetRequestForHighScores(urlBackendHighScores));
    }

    // Method to post the player's game results to the backend
    public void PostGameResults()
    {
        // Create a HighScore object with player data
        HighScores.HighScore highScore = new HighScores.HighScore();

        string formattedTimeString = Timer.LastRecordedTime;

        // Convert the formatted time string to total seconds using Timer's method
        float totalTimeInSeconds = Timer.ConvertFormattedTimeToSeconds(formattedTimeString);
        highScore.score = totalTimeInSeconds;
        
        // Convert PlayerNameInput.playerName to string and assign it to playername
        highScore.playername = PlayerNameInput.playerName != null ? PlayerNameInput.playerName.ToString() : "PlayerNameIsNull";

        Debug.Log("PostGameResults called " + highScore.playername + " with scores " + totalTimeInSeconds);
        Debug.Log(highScore.score);
        // Send a POST request to the backend with the player's game results
        StartCoroutine(PostRequestForHighScores(urlBackendHighScores, highScore));
    }




    // Method to create a formatted string representing the top three high scores
    string CreateHighScoreList()
    {
        string hsList = "";
        if (hs != null)
        {
            int len = (hs.scores.Length < 3) ? hs.scores.Length : 3;
            for (int i = 0; i < len; i++)
            {
                // Format and append each high score to the string
                hsList += string.Format("[ {0} ] {1,15} {2,5} {3,15}\n",
                (i + 1), hs.scores[i].playername, hs.scores[i].score, hs.scores[i].playtime);
            }
        }
        return hsList;
    }

    // Method to insert a log message and update the log variable
    string InsertToLog(string s)
    {
        return log = "[" + fetchCounter + "]" + s + "\n" + log;
    }

    // Coroutine for sending a GET request to fetch high scores from the backend
    IEnumerator GetRequestForHighScores(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            InsertToLog("Request sent to " + uri);

            // Set downloadHandler for json
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");

            // Request and wait for reply
            yield return webRequest.SendWebRequest();

            // Get raw data and convert it to string
            string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

            if (webRequest.isNetworkError)
            {
                // Log an error if there is a network error
                InsertToLog("Error encountered: " + webRequest.error);
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // Deserialize the received JSON string to update the HighScores object
                hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);
                updateHighScoreTextArea = true;

                // Log success and received data
                InsertToLog("Response received successfully ");
                Debug.Log("Received(UTF8): " + resultStr);
                Debug.Log("Received(HS): " + JsonUtility.ToJson(hs));
            }
        }
    }

    // Coroutine for sending a POST request to submit player's game results to the backend
    IEnumerator PostRequestForHighScores(string uri, HighScores.HighScore hsItem)
    {
        
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(uri, JsonUtility.ToJson(hsItem)))
        {
            InsertToLog("POST request sent to " + uri);

            // Set downloadHandler for json
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");

            // Request and wait for reply
            yield return webRequest.SendWebRequest();

            // Get raw data and convert it to string
            string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

            if (webRequest.isNetworkError)
            {
                // Log an error if there is a network error during the POST request
                InsertToLog("Error encountered in post request: " + webRequest.error);
                Debug.Log("Error: " + webRequest.error);
                yield break;
            }
            else
            {
                // Log success and received data for the POST request
                InsertToLog("POST request successful");
                Debug.Log("Received(UTF8): " + resultStr);

                // Check if the desired response is received
                if (resultStr.Contains("{\"status\":\"OK\",\"dbg\":\"POST received:"))
                {
                    // Run FetchhighScoresJSON if the desired response is received
                    FetchhighScoresJSON();
                }
            }
        }
    }

    // Additional methods or coroutines can be added here...

}
