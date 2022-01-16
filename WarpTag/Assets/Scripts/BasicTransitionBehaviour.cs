using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicTransitionBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject sceneCurtain;
    private Animator curtainAnimator;
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private string animationTrigger;
    [SerializeField]
    private float transitionAnimationDuration;

    private void Awake()
    {
        curtainAnimator = sceneCurtain.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            IEnumerator sceneChangeRoutine = changeScene(nextSceneName);
            StartCoroutine(sceneChangeRoutine);
        }
    }

    private IEnumerator changeScene(string sceneName)
    {
        curtainAnimator.SetTrigger(animationTrigger);
        yield return new WaitForSeconds(transitionAnimationDuration);
        SceneManager.LoadScene(sceneName);
    }
}
