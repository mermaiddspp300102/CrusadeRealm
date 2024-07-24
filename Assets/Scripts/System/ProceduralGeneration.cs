using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject startRoomPrefab;
    public GameObject[] roomPrefabs;
    public GameObject endRoomPrefab;
    public int numberOfRooms = 10;
    void Start()
    {
        Vector2 spawnPosition = Vector2.zero;
        float totalRoomWidth = 0f;

        totalRoomWidth += CreateRoom(startRoomPrefab, spawnPosition).width;
        for (int i = 0; i < numberOfRooms - 2; i++)
        {
            GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
            totalRoomWidth += CreateRoom(roomPrefab, spawnPosition + new Vector2(totalRoomWidth, 0)).width + 0;
        }
        totalRoomWidth += CreateRoom(endRoomPrefab, spawnPosition + new Vector2(totalRoomWidth, 0)).width;
    }
    private RoomDimension CreateRoom(GameObject roomPrefab, Vector2 position)
    {
        GameObject roomInstance = Instantiate(roomPrefab, position, Quaternion.identity);
        return roomInstance.GetComponent<RoomDimension>();
    }
}
