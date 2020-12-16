using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{    
    public int Score { get => score; set { score = value; updateScoreBoard(score); } }

    private int score;
    private Text ScoreText;
    void Start()
    {
        ScoreText = GetComponent<Text>();
        Score = 0;
    }

    public void ScoreHit(int scoreValue)
    {
        Score += scoreValue;
    }

    private void updateScoreBoard(int score)
    {
        ScoreText.text = score.ToString();
    }
}
