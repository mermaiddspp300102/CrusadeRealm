using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomDimension : MonoBehaviour
{
    public float width { get; private set; }
    public float height { get; private set; }

    void Awake()
    {
        Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();

        float minX = Mathf.Infinity;
        float minY = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float maxY = Mathf.NegativeInfinity;

        foreach (Tilemap tilemap in tilemaps)
        {
            BoundsInt bounds = tilemap.cellBounds;
            Vector3Int position = tilemap.origin;

            minX = Mathf.Min(minX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxX = Mathf.Max(maxX, position.x + bounds.size.x);
            maxY = Mathf.Max(maxY, position.y + bounds.size.y);
        }

        width = maxX - minX;
        height = maxY - minY;
    }
}
