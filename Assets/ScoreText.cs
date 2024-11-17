using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    //表示用テキスト
    private int score_;
    //テキスト本体
    private TMP_Text scoreText_;
    private void Start()
    {
        score_ = 0;
        scoreText_ = GetComponent<TMP_Text>();
        
    }
    public void SetScore(int score)
    { score_ = score;
        UpdateScoreText();
    }
    // Update is called once per frame
    private void UpdateScoreText()
    {
        scoreText_.text = $"SCORE:{score_:0000000}";   
    }
}
