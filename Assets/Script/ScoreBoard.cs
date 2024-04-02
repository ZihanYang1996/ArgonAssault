using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    // Start is called before the first frame update
    int _score = 0;
    TextMeshProUGUI scoreText;

    public int score
    {
        get
        {
            return _score;
        }
    }

    public void IncreaseScore(int amount)
    {
        _score += amount;
        scoreText.text = _score.ToString("D4");
    }

    void Awake()
    {
        scoreText = gameObject.GetComponentInChildren<TextMeshProUGUI>();   
    }
}
