using System.Collections;
using System.Collections.Generic;
using FotF.Api.Enums;
using FotF.Api.Enums.Units;
using FotF.Api.Models.Troopers;
using FotF.Api.Prisms;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrooperController : MonoBehaviour
{

    #region Trooper Config
    public FactionType Faction { get; set; }
    public TrooperClass Class { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    #endregion

    public float moveSpeed = 100f; // Speed of the trooper
    public Vector2 tileSize = new Vector2(1, 2); // Size of the trooper in grid cells

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector3 targetPosition; // Target position for movement
    private bool isMoving = false; // Flag to check if the trooper is moving

    public Trooper Model;
    public IPrism Prism => Model.Config.Agent;


    void Start()
    {
        var config = new TrooperConfig(Faction, Class, FirstName, LastName);
        Model = new Trooper(config);
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position; // Initialize target position
    }

    void Update()
    {
        // If Troopers in Range:
        // - Attack Enemies
        // - Trade Allies
        // - Tend Self

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    public void SetTargetPosition(Vector3 position)
    {
        // Adjust the target position to align with the center of the 2x1 tile
        Vector3 adjustedPosition = new Vector3(
            position.x,
            position.y + (tileSize.y / 2f), // Offset upward by half the height of the trooper
            position.z
        );

        targetPosition = adjustedPosition;
        isMoving = true;
    }

    private void MoveToTarget()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);

        // Check if the target position is reached
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }
}
