using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;


public class BackendHandler : MonoBehaviour
{
    const string jsonTestStr = "{ " +
"\"scores\":[ " +
"{\"id\":1, \"playername\":\"Matti\", \"score\":20, \"playtime\": \"2020-21-11 08:20:00\"}, " +
"{\"id\":2, \"playername\":\"Hankka\", \"score\":30, \"playtime\": \"2020-21-11 08:20:00\"}, " +
"{\"id\":3, \"playername\":\"Ismo\", \"score\":40, \"playtime\": \"2020-21-11 08:20:00\"} " +
"] }";

    const string urlBackendHighScoresFile = "http://localhost/unityphpdemo/api/v1/highscores.json";
    const string urlBackendHighScores = "http://localhost/unityphpdemo/api/v1/highscores.php";
    HighScores.HighScores hs;

    // Logging info
    string log = "";
    int fetchCounter = 0;


    // UI elements

    public UnityEngine.UI.Text loggingText;
    public UnityEngine.UI.InputField PlayerNameInputField;
    public UnityEngine.UI.InputField ScoreInputField;
    public UnityEngine.UI.Text highScoresTextArea;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BackendHandler started");

        hs = JsonUtility.FromJson<HighScores.HighScores>(jsonTestStr);
        Debug.Log("HighScore winner name: " + hs.scores[0].playername);
        Debug.Log("HighScores as json:" + JsonUtility.ToJson(hs));

        InsertToLog("Game started");
    }

    bool updateHighScoreTextArea = false;
    // Update is called once per frame
    void Update()
    {
        loggingText.text = log;

        if (updateHighScoreTextArea)
        {
            highScoresTextArea.text = CreateHighScoreList();
            updateHighScoreTextArea = false;
        }
    }

    public void FetchhighScoresJSONFile()
    {
        fetchCounter++;
        Debug.Log("JSONfile Clicked");
        StartCoroutine(GetRequestForHighScores(urlBackendHighScoresFile));
    }
    public void FetchhighScoresJSON()
    {
        fetchCounter++;
        Debug.Log("JSONfile Clicked");
        StartCoroutine(GetRequestForHighScores(urlBackendHighScores));
    }
    public void PostGameResults()
    {
        HighScores.HighScore highScore = new HighScores.HighScore();
        if (PlayerNameInputField.text.Length > 0 && ScoreInputField.text.Length > 0)
        {
            highScore.playername = PlayerNameInputField.text;
            highScore.score = float.Parse(ScoreInputField.text);

           
            Debug.Log("PostGameResults called" + PlayerNameInputField.text + "with scores" + ScoreInputField.text);
            StartCoroutine(PostRequestForHighScores(urlBackendHighScores, highScore));
        }
     


    }
    string CreateHighScoreList()
    {

        string hsList = "";
        if(hs != null)
        {
            int len = (hs.scores.Length < 3) ?  hs.scores.Length : 3;
            for(int i = 0; i < len; i++)
            {
                hsList += hs.scores[i].playername + ": \t" +
                hs.scores[i].score + " \t" + hs.scores[i].playtime + "\n";
               
                hsList += string.Format("[ {0} ] {1,15} {2,5} {3,15}\n",
                (i + 1), hs.scores[i].playername, hs.scores[i].score, hs.scores[i].playtime);

            }
        }
        return hsList;
    }
    string InsertToLog(string s)
    {
        return log = "[" + fetchCounter + "]" + s + "\n" + log;
    }


    // ...

    IEnumerator GetRequestForHighScores(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            InsertToLog("Request sent to " + uri);
            // set downloadHandler for json
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");
            // Request and wait for reply
            yield return webRequest.SendWebRequest();
            // get raw data and convert it to string
            string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
            if (webRequest.isNetworkError)
            {
                InsertToLog("Error encountered: " + webRequest.error);
                Debug.Log("Error: " + webRequest.error); // Added debug log
            }
            else
            {
                // create HighScore item from json string
                hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);
                updateHighScoreTextArea = true;
                InsertToLog("Response received succesfully ");
                Debug.Log("Received(UTF8): " + resultStr); // Added debug log
                Debug.Log("Received(HS): " + JsonUtility.ToJson(hs)); // Added debug log
            }
        }
    }

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
                InsertToLog("Error encountered in post request: " + webRequest.error);
                Debug.Log("Error: " + webRequest.error); // Added debug log
                yield break;
            }
            else
            {
                InsertToLog("POST request successful");
                Debug.Log("Received(UTF8): " + resultStr); // Added debug log
            }
        }
    }

    // ...



}

