using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerBehaviour : MonoBehaviour
{
    private Slider barFill;
    [SerializeField]
    private float baseGameTime = 10f;
    [SerializeField]
    private GameData gameData;
    private float currentGameTime;
    [SerializeField]
    private GameObject sceneCurtain;
    private Animator curtainAnimator;
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private string animationTrigger;
    [SerializeField]
    private float transitionAnimationDuration;
    private bool gameStart = false;

    private void Awake()
    {
        barFill = GetComponent<Slider>();
        currentGameTime = baseGameTime;
        curtainAnimator = sceneCurtain.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startTimerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            updateCurrentTime();
            updateBarFill();
        }
    }

    private IEnumerator startTimerRoutine()
    {
        yield return new WaitForSeconds(transitionAnimationDuration);
        gameStart = true;
    }

    private void updateCurrentTime()
    {
        currentGameTime -= Time.deltaTime;
        if (currentGameTime <= 0f)
        {
            StartCoroutine(transitionToGameOver());
        }
    }

    private IEnumerator transitionToGameOver()
    {
        gameData.StopGameTime();
        curtainAnimator.SetTrigger(animationTrigger);
        yield return new WaitForSeconds(transitionAnimationDuration);
        SceneManager.LoadScene(nextSceneName);
    }

    private void updateBarFill()
    {
        float remainingTimeRatio = currentGameTime / baseGameTime;
        barFill.value = remainingTimeRatio;
    }

    public void AddAdditionalTime(float timeToAdd)
    {
        float newTime = currentGameTime + timeToAdd;
        if (newTime > baseGameTime)
        {
            currentGameTime = baseGameTime;
        }
        else
        {
            currentGameTime = newTime;
        }
    }
}
