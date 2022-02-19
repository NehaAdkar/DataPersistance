using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public string CurrentName;
    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject TextBox;
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    public int highScore;
    public string highScoreName;

    public TextMeshProUGUI BestScoreText;
    public TMP_InputField InputName;

    public string WinnerName;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoadScore();
        WinnerName="neha";
        HighScoreDisplay();
        
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
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
                HighScoreDisplay();
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
                GameManager.Instance.SaveScore();
                SceneManager.LoadScene(1);
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
        CheckHighScore();
        HighScoreDisplay();
        GameOverText.SetActive(true);
        GameManager.Instance.SaveScore();
    }

    public void HighScoreDisplay()
    {
        GameManager.Instance.LoadScore();
        BestScoreText.text = $"Best Score : {WinnerName} : {highScore}";
        highScore = GameManager.Instance.Score;
        highScoreName = GameManager.Instance.Name;
        
    }

    public void NameBox()
    {
        WinnerName=InputName.text;
    }

    public void Reset()
    {
        highScore=0;
    }


    public void CheckHighScore()
    {
        if(m_Points > highScore){
            highScore = m_Points;
            TextBox.SetActive(true);
            
            GameManager.Instance.Score = highScore;
            GameManager.Instance.Name = WinnerName;
            GameManager.Instance.SaveScore();
        }
    }
}
