using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPickupBehaviour : MonoBehaviour, PickupItem
{
    private const float collectionBonus = 2f;

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
        PlayerCollisionHandler playerObject;
        if (collision.gameObject.TryGetComponent<PlayerCollisionHandler>(out playerObject))
        {
            UpdateGameData();
            UpdatePlayerAttributes();
            StartCoroutine(destroyRoutine());
        }
    }

    public void SetLocation(Vector2 startLocation)
    {
        this.transform.position = startLocation;
    }

    public void UpdateGameData()
    {
        GameData gameData = FindObjectOfType<GameData>();
        gameData.IncrementBonusCount();
    }

    public void UpdatePlayerAttributes()
    {
        TimerBehaviour timer = FindObjectOfType<TimerBehaviour>();
        timer.AddAdditionalTime(collectionBonus);
    }

    private IEnumerator destroyRoutine()
    {
        Destroy(this.gameObject);
        yield return null;
    }
}
