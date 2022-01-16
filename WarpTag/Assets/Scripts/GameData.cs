using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private bool created = false;
    public int NumberOfTags { get; private set; }
    public int NumberOfBonuses { get; private set; }
    public int NumberOfSeconds { get; private set; }

    private bool shouldGameTimerStop = false;
    private float totalGameTime = 0f;

    private void Awake()
    {
        if (!created)
        {
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldGameTimerStop)
        {
            totalGameTime += Time.deltaTime;
        }
    }

    public void IncrementTagCount()
    {
        NumberOfTags++;
    }

    public void IncrementBonusCount()
    {
        NumberOfBonuses++;
    }

    public void StopGameTime()
    {
        shouldGameTimerStop = true;
    }

    public int GenerateScore()
    {
        calculateNumberOfSeconds();
        return NumberOfTags * 100 + NumberOfBonuses * 50 + NumberOfSeconds * 10;
    }

    private void calculateNumberOfSeconds()
    {
        NumberOfSeconds = (int)totalGameTime;
    }
}
