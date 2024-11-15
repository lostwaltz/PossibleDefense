using UnityEngine;
using UnityEngine.Serialization;

public class MapGenerator : MonoBehaviour
{
    public GameObject tilePrefabNone;
    
    public int width = 10;        
    public int height = 10;        
    public Grid grid;
    public Camera mainCamera;

    private void Start()
    {
        GenerateTileMap();
        mainCamera.transform.position = new Vector3(width / 2f, 15, height / 2f);
        mainCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    private void GenerateTileMap()
    {
        for (var x = 0; x < width; x++)
        {
            for (var z = 0; z < height; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, z);
                Vector3 worldPosition = grid.CellToWorld(cellPosition);

                Instantiate(tilePrefabNone, worldPosition, Quaternion.identity, this.transform);
            }
        }
    }
}