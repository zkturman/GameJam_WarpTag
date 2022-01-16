using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardWarpBehaviour : MonoBehaviour
{
    [SerializeField]
    private float baseWarpDistance = 5f;
    [SerializeField]
    private float baseWarpIncrement = 5f;
    [SerializeField]
    private GameData gameData;
    [SerializeField]
    private RoomBehaviour roomData;
    [SerializeField]
    private int numberOfWarpPoints = 10;
    [SerializeField]
    private GameObject wizardObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WarpWizard()
    {
        Vector2 newDestination = generateWarpDestination();
        IEnumerator warpRoutine = warpWizard(newDestination);
        StartCoroutine(warpRoutine);
    }

    private Vector2 generateWarpDestination()
    {
        List<Vector2> allWarpPoints = generateWarpPoints();
        List<Vector2> validWarpPoints = removeInvalidWarpPoints(allWarpPoints);
        int diceRoll = Random.Range(0, validWarpPoints.Count);
        return validWarpPoints[diceRoll];
    }

    private List<Vector2> generateWarpPoints()
    {
        float degreeIncrement = 360f / numberOfWarpPoints;
        float warpDistance = calculateWarpDistance();
        List<Vector2> warpPoints = new List<Vector2>();
        for (int i = 0; i < numberOfWarpPoints; i++)
        {
            float degreesAroundCenter = i * degreeIncrement;
            Vector2 newWarpPoint = calculateWizardWarpPoint(warpDistance, degreesAroundCenter);
            warpPoints.Add(newWarpPoint);
        }
        return warpPoints;
    }
    private float calculateWarpDistance()
    {
        float randomFactor = Random.Range(0.9f, 1.1f);
        float logFactor = Mathf.Log(gameData.NumberOfTags + baseWarpIncrement, baseWarpIncrement);
        return baseWarpDistance * randomFactor * logFactor;
    }

    private Vector2 calculateWizardWarpPoint(float distance, float degreesAroundCenter)
    {
        Vector2 newWarpPoint = calcuateBaseWarpPoint(distance, degreesAroundCenter);
        newWarpPoint.x += transform.position.x;
        newWarpPoint.y += transform.position.y;
        return newWarpPoint;
    }

    private Vector2 calcuateBaseWarpPoint(float distance, float degreesAroundCenter)
    {
        float radiansAroundCenter = degreesAroundCenter * Mathf.Deg2Rad;
        float xCoordinate = Mathf.Cos(radiansAroundCenter);
        float yCoordinate = Mathf.Sin(radiansAroundCenter);
        return new Vector2(xCoordinate, yCoordinate) * distance;
    }

    private List<Vector2> removeInvalidWarpPoints(List<Vector2> allWarpPoints)
    {
        List<Vector2> validWarpPoints = new List<Vector2>();
        foreach (Vector2 vector in allWarpPoints)
        {
            if (roomData.IsInBounds(vector.x, vector.y))
            {
                validWarpPoints.Add(vector);
            }
        }
        return validWarpPoints;
    }

    private IEnumerator warpWizard(Vector2 destination)
    {
        wizardObject.transform.position = destination;
        wizardObject.GetComponentInChildren<Rigidbody2D>().velocity = Vector2.zero;
        yield return null;
    }
}
