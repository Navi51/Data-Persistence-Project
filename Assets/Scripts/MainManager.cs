using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text ScoreTextFinal; // final score
    public string Name; // name from menu
    public string NameFinal; // name for save

    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    public int m_PointsFinal;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerName();
        //LoadPlayerScore();
        // ScoreTextFinal.text = $"Best score of {NameFinal} {m_PointsFinal}";
        if (NameFinal == MenuUI.playerName)
        {
            LoadPlayerScore();
            ScoreTextFinal.text = $"Best score of {NameFinal} {m_PointsFinal}";
        }
        Name = MenuUI.playerName;


        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        m_PointsFinal = m_Points;
        NameFinal = Name;

        ScoreTextFinal.text = $"Best score of {NameFinal} {m_PointsFinal}";
        SavePlayerName();
        SavePlayerScore();

    }

    class SaveData
    {
        public string NameFinal;
        public int m_PointsFinal;
    }
    

    public void SavePlayerName()
    {
        SaveData data = new SaveData();
        data.NameFinal = NameFinal;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            NameFinal = data.NameFinal;
        }
    }
    public void SavePlayerScore()
    {
        SaveData dataScore = new SaveData();
        dataScore.m_PointsFinal = m_PointsFinal;
        string json = JsonUtility.ToJson(dataScore);
        File.WriteAllText(Application.persistentDataPath + "/savescore.json", json);
    }
    public void LoadPlayerScore()
    {
        string path = Application.persistentDataPath + "/savescore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData dataScore = JsonUtility.FromJson<SaveData>(json);

            m_PointsFinal = dataScore.m_PointsFinal;
        }
    }
}
