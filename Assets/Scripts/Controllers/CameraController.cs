using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float zoomSpeed = 2f;
    public LayerMask trooperLayer;
    public GameObject moveMarkerPrefab; // Assign your MoveMarker prefab here

    private List<GameObject> selectedTroopers = new List<GameObject>();
    private Vector2 startMousePosition;
    private GridController gridController; // Reference to the GridController

    void Start()
    {
        gridController = FindObjectOfType<GridController>();
        if (gridController == null)
        {
            Debug.LogError("GridController not found! Ensure there is a GridController in the scene.");
        }
    }

    void Update()
    {
        HandleCameraMovement();
        HandleTrooperSelection();
    }

    private void HandleCameraMovement()
    {
        // Pan camera
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(movement * panSpeed * Time.deltaTime);

        // Zoom camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5f, 20f); // Clamp zoom levels
    }

    private void HandleTrooperSelection()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            startMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            SelectTroopersInBox();
        }

        if (Input.GetMouseButtonDown(1) && selectedTroopers.Count > 0) // Right click
        {
            MoveSelectedTroopers();
        }
    }

    private void SelectTroopersInBox()
    {
        // Clear previous selection
        DeselectTroopers();

        // Convert screen coordinates to world coordinates
        Vector2 worldStart = Camera.main.ScreenToWorldPoint(startMousePosition);
        Vector2 worldEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Find all troopers in the selection box
        Collider2D[] colliders = Physics2D.OverlapAreaAll(worldStart, worldEnd, trooperLayer);
        foreach (Collider2D collider in colliders)
        {
            GameObject trooper = collider.gameObject;
            HighlightTrooper(trooper);
            selectedTroopers.Add(trooper);
        }
    }

    private void MoveSelectedTroopers()
    {
        // Get target position from mouse click
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = gridController.GetGrid().WorldToCell(mouseWorldPos);

        // Ensure we are moving to a valid tile center
        if (gridController.HasTile(gridPosition))
        {
            // Place the move marker
            PlaceMoveMarker(gridController.GetGrid().GetCellCenterWorld(gridPosition));

            // Command the selected troopers to move to the center of the tile
            foreach (var trooper in selectedTroopers)
            {
                TrooperController mover = trooper.GetComponent<TrooperController>();
                if (mover != null)
                {
                    // Update the trooper's target to the center of the tile
                    mover.SetTargetPosition(gridController.GetGrid().GetCellCenterWorld(gridPosition));
                }
            }
        }
        else
        {
            Debug.LogWarning("Invalid tile position for trooper movement.");
        }
    }

    private void PlaceMoveMarker(Vector2 position)
    {
        if (moveMarkerPrefab != null)
        {
            // Instantiate the marker and destroy it after 1.5 seconds
            GameObject marker = Instantiate(moveMarkerPrefab, position, Quaternion.identity);
            Destroy(marker, 1.5f);
        }
    }

    private void DeselectTroopers()
    {
        foreach (var trooper in selectedTroopers)
        {
            DeselectTrooper(trooper);
        }
        selectedTroopers.Clear();
    }

    private void HighlightTrooper(GameObject trooper)
    {
        SpriteRenderer spriteRenderer = trooper.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.cyan; // Highlight color
        }
    }

    private void DeselectTrooper(GameObject trooper)
    {
        SpriteRenderer spriteRenderer = trooper.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white; // Default color
        }
    }
}
