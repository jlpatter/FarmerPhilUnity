using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnerBehavior : MonoBehaviour {
    public GameObject wheatPrefab;
    public Tilemap tilemap;

    private GameObject _wheatField;

    // Start is called before the first frame update
    private void Start() {
        _wheatField = GameObject.FindGameObjectWithTag("WheatField");
        SpawnWheat();
    }

    private void SpawnWheat() {
        // Find wheat's x start location
        var wheatXStartLocation = 0;
        while (tilemap.GetSprite(new Vector3Int(wheatXStartLocation, 0, 0)).name.Equals("dirt")) {
            wheatXStartLocation--;
        }
        wheatXStartLocation++;

        // Find wheat's y start location
        var wheatYLocation = 0;
        while (tilemap.GetSprite(new Vector3Int(0, wheatYLocation, 0)).name.Equals("dirt")) {
            wheatYLocation--;
        }
        wheatYLocation++;

        var wheatSize = wheatPrefab.GetComponent<SpriteRenderer>().bounds.size;
        var wheatXLocation = wheatXStartLocation;

        while (tilemap.GetSprite(new Vector3Int(0, wheatYLocation, 0)).name.Equals("dirt")) {
            while (tilemap.GetSprite(new Vector3Int(wheatXLocation, 0, 0)).name.Equals("dirt")) {
                Instantiate(wheatPrefab, new Vector3(wheatXLocation + wheatSize.x / 2.0f, wheatYLocation + wheatSize.y / 2.0f, 0.0f), Quaternion.identity, _wheatField.transform);
                Instantiate(wheatPrefab, new Vector3(wheatXLocation + 0.5f + wheatSize.x / 2.0f, wheatYLocation + wheatSize.y / 2.0f, 0.0f), Quaternion.identity, _wheatField.transform);
                wheatXLocation++;
            }
            wheatXLocation = wheatXStartLocation;
            wheatYLocation++;
        }
    }
}
