using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject NorthWall;
    [SerializeField]
    private GameObject SouthWall;
    [SerializeField]
    private GameObject WestWall;
    [SerializeField]
    private GameObject EastWall;
    public float MaxHeight { get; private set; }
    public float MinHeight { get; private set; }
    public float MaxWidth { get; private set; }
    public float MinWidth { get; private set; }

    void Awake()
    {
        MaxHeight = NorthWall.transform.position.y - (NorthWall.transform.localScale.y / 2);
        MinHeight = SouthWall.transform.position.y + (SouthWall.transform.localScale.y / 2);
        MaxWidth= EastWall.transform.position.x - (EastWall.transform.localScale.x / 2);
        MinWidth = WestWall.transform.position.x + (WestWall.transform.localScale.x / 2);

        Debug.Log(MaxHeight + " " + MinHeight + " " + MaxWidth + " " + MinWidth);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsInBounds(float x, float y)
    {
        bool returnFlag = true;
        if (x > MaxWidth || x < MinWidth)
        {
            returnFlag = false;
        }
        if (y > MaxHeight || y < MinHeight)
        {
            returnFlag = false;
        }
        return returnFlag;
    }

    public Vector2 GenerateRandomRoomPosition()
    {
        float xCoordinate = Random.Range(MinWidth, MaxWidth);
        float yCoordinate = Random.Range(MinHeight, MaxHeight);
        return new Vector2(xCoordinate, yCoordinate);
    }
}
