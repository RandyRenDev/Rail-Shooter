using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    [SerializeField] Text uiScore;

    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        uiScore.text = score.ToString();
    }


    public void updateScore()
    {
        score = score + 10;
        uiScore.text = score.ToString();
    }
}
