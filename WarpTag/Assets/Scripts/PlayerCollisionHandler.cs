using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;
    [SerializeField]
    private TimerBehaviour timerBehaviour;
    [SerializeField]
    private float tagTimeBonus = 2f;
    [SerializeField]
    private GameObject[] pickupItems;
    [SerializeField]
    private float pickupChance = 0.1f;
    [SerializeField]
    private RoomBehaviour roomData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        WizardWarpBehaviour wizard = collision.gameObject.GetComponentInChildren<WizardWarpBehaviour>();
        if (wizard != null)
        {
            wizard.WarpWizard();
            gameData.IncrementTagCount();
            generatePickupItem();
            timerBehaviour.AddAdditionalTime(tagTimeBonus);
        }
    }

    private void generatePickupItem()
    {
        if (shouldGeneratePickupItem() && pickupItems.Length > 0)
        {
            instantiatePickupItem();
        }
    }

    private bool shouldGeneratePickupItem()
    {
        float diceRoll = Random.Range(0f, 1f);
        return diceRoll < pickupChance;
    }

    private void instantiatePickupItem()
    {
        int diceRoll = Random.Range(0, pickupItems.Length);
        GameObject pickupItem = pickupItems[diceRoll];
        Instantiate(pickupItem, determineItemLocation(), Quaternion.identity);
    }

    private Vector2 determineItemLocation()
    {
        return roomData.GenerateRandomRoomPosition();
    }
}
