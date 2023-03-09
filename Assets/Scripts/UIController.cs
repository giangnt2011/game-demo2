using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private Text ScoreText;


    private void Awake()
    {
        instance = this;
        //ScoreText.text = PlayerPrefs.GetInt("score").ToString();
    }
    

    public void SetScoreTxt(int score)
    {
        ScoreText.text = (PlayerPrefs.GetInt("score") + score).ToString();
    }

    public int GetScoreTxt()
    {
        return Convert.ToInt32(ScoreText.text);
    }
}
