using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PickupItem
{
    public void SetLocation(Vector2 startLocation);
    public void UpdateGameData();
    public void UpdatePlayerAttributes();
}
