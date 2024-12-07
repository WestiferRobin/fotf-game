using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    private Grid grid; // Reference to the Grid component
    public Grid GetGrid() { return grid ?? null; }
    private Tilemap tilemapFloors; // Floors tilemap
    private Tilemap tilemapWalls; // Walls tilemap
    private Tilemap tilemapDoors; // Doors tilemap
    private Tilemap tilemapHighlight; // Highlight tilemap
    private Tilemap tilemapInteractables;

    public TileBase highlightTile; // Tile to use for highlights
    private Vector3Int lastHighlightedCell; // Track the last highlighted cell

    void Awake()
    {
        grid = GetComponent<Grid>();
        if (grid == null)
        {
            Debug.LogError("Grid component not found!");
        }

        // Cache all tilemap references at the start
        tilemapHighlight = GetTilemap("Highlight");
        tilemapFloors = GetTilemap("Floors");
        tilemapWalls = GetTilemap("Walls");
        tilemapDoors = GetTilemap("Doors");
        tilemapInteractables = GetTilemap("Interactables");

        // Check if all tilemaps were successfully found
        if (tilemapHighlight == null || tilemapFloors == null || tilemapWalls == null || tilemapDoors == null)
        {
            Debug.LogError("One or more tilemaps are missing! Ensure the names match the tags.");
        }
    }

    void Update()
    {
        HandleMouseHighlight();
    }

    private Tilemap GetTilemap(string tag)
    {
        var tilemapObject = GetTilemapObject(tag);
        if (tilemapObject == null)
        {
            Debug.LogWarning($"Tilemap object with tag '{tag}' not found.");
            return null;
        }

        return tilemapObject.GetComponent<Tilemap>();
    }

    private GameObject GetTilemapObject(string tag)
    {
        string fullTag = $"Tilemap_{tag}";
        var tilemapTransform = transform.Find(fullTag);
        if (tilemapTransform == null)
        {
            Debug.LogWarning($"Transform with name '{fullTag}' not found under Grid.");
            return null;
        }

        return tilemapTransform.gameObject;
    }

    public bool HasTile(Vector3Int target)
    {
        return tilemapFloors.HasTile(target) || tilemapDoors.HasTile(target) || tilemapInteractables.HasTile(target);
    }

    private void HandleMouseHighlight()
    {
        if (tilemapHighlight == null || tilemapFloors == null) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = grid.WorldToCell(mouseWorldPos);

        // Only update highlight if the mouse is over a floor tile
        if (HasTile(cellPosition))
        {
            // Clear the previous highlight if it's a different cell
            if (cellPosition != lastHighlightedCell)
            {
                tilemapHighlight.ClearAllTiles();
                tilemapHighlight.SetTile(cellPosition, highlightTile);
                lastHighlightedCell = cellPosition;
            }
        }
        else
        {
            // Clear the highlight if the cursor is not over a floor tile
            tilemapHighlight.ClearAllTiles();
            lastHighlightedCell = Vector3Int.zero;
        }
    }
}
