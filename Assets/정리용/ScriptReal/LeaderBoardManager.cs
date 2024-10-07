using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    private string filePath = "leaderboard.json"; // JSON 파일 경로

    private static LeaderboardManager instance;

    public string currentName;
    public bool fisrtScene;

    
    public TMP_Text[] RankNameText;
    
    public TMP_Text[] RankScoreText;

    public TMP_Text[] RankKillText;

    public Image[] RankCHImage;

    public Sprite[] SpriteCH;

    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int score;
        public string gameTime;
        public int chID;
        public int kill;
    }

    [System.Serializable]
    private class PlayerDataList
    {
        public List<PlayerData> players;
    }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static LeaderboardManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

   
    
    void Start()
    {
        
        if(fisrtScene)
        {
            LoadLeaderboard();
        }
    }

    void LoadLeaderboard()
    {
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            PlayerDataList playerDataList = JsonUtility.FromJson<PlayerDataList>(jsonString);

            if (playerDataList != null && playerDataList.players != null)
            {
                // 플레이어 순위 정렬
                playerDataList.players.Sort((x, y) => x.score.CompareTo(y.score));

                // 랭킹 표시
                for (int i = 0; i < playerDataList.players.Count; i++)
                {
                    RankNameText[i].text = playerDataList.players[i].name;
                    RankScoreText[i].text = playerDataList.players[i].gameTime;
                    RankCHImage[i].sprite = SpriteCH[playerDataList.players[i].chID];
                    RankCHImage[i].color = new Color(1, 1, 1, 1);

                    RankKillText[i].text = playerDataList.players[i].kill.ToString();
                }
            }
            else
            {
                Debug.LogError("Failed to load leaderboard data.");
            }
        }
        else
        {
            Debug.LogWarning("Leaderboard JSON file not found. Creating a new one.");

            // 새로운 플레이어 데이터 생성
            PlayerDataList playerDataList = new PlayerDataList();
            playerDataList.players = new List<PlayerData>();

            // JSON 파일에 저장
            string defaultJsonString = JsonUtility.ToJson(playerDataList);
            File.WriteAllText(filePath, defaultJsonString);

            Debug.Log("New leaderboard JSON file created.");
        }
    }


    public void SaveScore(string playerName, int newScore, string gameTime, int chID,int kill)
    {
        string jsonString = File.ReadAllText(filePath);
        PlayerDataList playerDataList = JsonUtility.FromJson<PlayerDataList>(jsonString);

        if (playerDataList != null && playerDataList.players != null)
        {
            // 새로운 플레이어 데이터 생성
            PlayerData newPlayerData = new PlayerData();
            newPlayerData.name = playerName;
            newPlayerData.score = newScore;
            newPlayerData.gameTime = gameTime;
            newPlayerData.chID = chID;
            newPlayerData.kill = kill;

            // 기존 플레이어 목록에 추가
            playerDataList.players.Add(newPlayerData);

            // 플레이어 순위 재정렬
            playerDataList.players.Sort((x, y) => y.score.CompareTo(x.score));

           

            // JSON 파일에 저장
            string updatedJsonString = JsonUtility.ToJson(playerDataList);
            File.WriteAllText(filePath, updatedJsonString);

            Debug.Log("Score saved successfully.");
        }
        else
        {
            Debug.LogError("Failed to save score.");
        }
    }
}