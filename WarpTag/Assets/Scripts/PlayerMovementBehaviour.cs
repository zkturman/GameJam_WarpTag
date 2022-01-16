using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 20f;
    private float currentSpeed;
    [SerializeField]
    public float SpeedModifier = 1.1f;
    private const float diagonalSpeedModifier = 0.7f;
    private Rigidbody2D playerRigidBody;

    private void Awake()
    {
        playerRigidBody = GetComponentInParent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = baseSpeed * SpeedModifier;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    public void UpdateSpeed()
    {
        currentSpeed *= SpeedModifier;
    }

    private void movePlayer()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        playerRigidBody.velocity = createPlayerMovement(horizontalMovement, verticalMovement) * baseSpeed;

    }

    private Vector2 createPlayerMovement(float horizontalMovement, float verticalMovement)
    {
        if (horizontalMovement != 0 && verticalMovement != 0)
        {
            horizontalMovement *= diagonalSpeedModifier;
            verticalMovement *= diagonalSpeedModifier;
        }

        return new Vector2(horizontalMovement, verticalMovement);
    }
}
