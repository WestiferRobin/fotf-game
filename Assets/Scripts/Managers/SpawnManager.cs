using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GridController gridController; // Reference to the GridController
    public GameObject trooperPrefab; // The trooper prefab

    public void Awake()
    {
        var board = GameObject.FindGameObjectWithTag("Board");
        gridController = board.GetComponent<GridController>();
    }

    public void Start()
    {
        SpawnTrooper(Vector3Int.zero);
    }

    public void SpawnTrooper(Vector3Int position)
    {
        // Check if the position is valid
        if (gridController.HasTile(position))
        {
            // Convert grid position to world position
            Vector3 worldPosition = gridController.GetGrid().GetCellCenterWorld(position);

            // Instantiate the trooper at the calculated position
            GameObject newTrooper = Instantiate(trooperPrefab, worldPosition, Quaternion.identity);
            newTrooper.name = "Trooper";
        }
        else
        {
            Debug.LogWarning("Invalid position for trooper spawn.");
        }
    }

    public void SpawnTroopers(List<Vector3Int> positions)
    {
        var set = new HashSet<string>();
        foreach (var position in positions)
        {
            var key = $"{position.x}: {position.y}";
            if (set.Contains(key)) continue;
            SpawnTrooper(position);
        }
    }
}
