using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class LevelFill_Enemies : MonoBehaviour
{
    public bool Other;
    public bool random_Amount;
    public int Least;
    public int Most;
    public RandomPlacementsOnTilemap Other_Title;
    public Tilemap availableSpotsTilemap; // Assign your Tilemap in the Unity Inspector
    public List<GameObject> objectToPlacePrefab; // Assign the GameObject prefab you wish to place
    public Transform Parent_Spawn; // Have all of the GameObjects stored in one space in the Hierarchy
    public int numberOfObjectsToPlace = 5; // The number of GameObjects you want to place
    List<Vector3Int> availablePositions = new List<Vector3Int>();
    TileBase[] allTiles;
    BoundsInt bounds;
    void Start()
    {
        if (Other)
        {
            Other_Title = GameObject.FindWithTag("Main_Spawner").GetComponent<RandomPlacementsOnTilemap>();
            availableSpotsTilemap = GameObject.FindWithTag("Field").GetComponent<Tilemap>();
            Parent_Spawn = GameObject.FindWithTag("E_Spawning_Location").GetComponent<Transform>();
        }
        if (random_Amount)
        {
            numberOfObjectsToPlace = Random.Range(Least, Most);
        }
        PlaceObjectsRandomly();
    }

    void PlaceObjectsRandomly()
    {
        if (!Other)
        {
            availablePositions = new List<Vector3Int>();

            // Loop through all the tiles in the Tilemap to find available spots
            bounds = availableSpotsTilemap.cellBounds;
            allTiles = availableSpotsTilemap.GetTilesBlock(bounds);

            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int localPosition = new Vector3Int(x, y, (int)availableSpotsTilemap.transform.position.z);
                    TileBase tile = availableSpotsTilemap.GetTile(localPosition);
                    if (tile != null) // If there's a tile, it's an available spot
                    {
                        availablePositions.Add(localPosition);
                    }
                }
            }

            // Randomly select positions to place objects
            for (int i = 0; i < numberOfObjectsToPlace; i++)
            {
                if (availablePositions.Count == 0)
                {
                    break;
                }

                int randomIndex = Random.Range(0, availablePositions.Count);
                Vector3Int selectedCellPosition = availablePositions[randomIndex];
                availablePositions.RemoveAt(randomIndex); // Remove to avoid duplicates

                // Convert the cell position to world position for instantiation
                Vector3 worldPosition = availableSpotsTilemap.CellToWorld(selectedCellPosition) + new Vector3(0.5f, 0.5f, 0); // Adjust for centering if necessary

                // Instantiate the GameObject at the selected position
                int Random_Enemey = Random.Range(0, objectToPlacePrefab.Count);
                (Instantiate(objectToPlacePrefab[Random_Enemey], worldPosition, Quaternion.identity) as GameObject).transform.parent = Parent_Spawn.transform;
            }
        }
        else
        {
            // Randomly select positions to place objects
            for (int i = 0; i < numberOfObjectsToPlace; i++)
            {
                if (Other_Title.availablePositions.Count == 0)
                {
                    break;
                }

                int randomIndex = Random.Range(0, Other_Title.availablePositions.Count);
                Vector3Int selectedCellPosition = Other_Title.availablePositions[randomIndex];
                Other_Title.availablePositions.RemoveAt(randomIndex); // Remove to avoid duplicates

                // Convert the cell position to world position for instantiation
                Vector3 worldPosition = Other_Title.availableSpotsTilemap.CellToWorld(selectedCellPosition) + new Vector3(0.5f, 0.5f, 0); // Adjust for centering if necessary

                // Instantiate the GameObject at the selected position
                int Random_Enemey = Random.Range(0, objectToPlacePrefab.Count);
                (Instantiate(objectToPlacePrefab[Random_Enemey], worldPosition, Quaternion.identity) as GameObject).transform.parent = Parent_Spawn.transform;
            }
        }


    }
}
