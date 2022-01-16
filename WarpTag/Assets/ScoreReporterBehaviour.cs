using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreReporterBehaviour : MonoBehaviour
{
    private GameData gameData;
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        gameData = FindObjectOfType<GameData>();
        scoreText = GetComponent<TextMeshProUGUI>();

    }
    // Start is called before the first frame update
    void Start()
    {
        if (gameData != null)
        {
            scoreText.text = "You scored " + gameData.GenerateScore().ToString() + ".";
        }
        else
        {
            scoreText.text = "You scored XXXX.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
