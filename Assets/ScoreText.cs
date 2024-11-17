using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    //�\���p�e�L�X�g
    private int score_;
    //�e�L�X�g�{��
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
