using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardDialogueBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<string> wizardSayings = new List<string>();
    private TextMeshProUGUI sayingText;

    private void Awake()
    {
        sayingText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dialogueRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator dialogueRoutine()
    {
        while (true)
        {
            int diceRoll = Random.Range(0, wizardSayings.Count);
            string sayingToDisplay = wizardSayings[diceRoll];
            sayingText.text = sayingToDisplay;
            yield return new WaitForSeconds(2f);
            sayingText.text = "";
            yield return new WaitForSeconds(3f);
        }
    }
}
