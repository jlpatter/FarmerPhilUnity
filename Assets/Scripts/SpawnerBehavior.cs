using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnerBehavior : MonoBehaviour {
    public GameObject wheatPrefab;
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start() {
        // Find wheat's x start location
        var tileNameX = "dirt";
        var wheatXStartLocation = 0;
        while (tileNameX.Equals("dirt")) {
            wheatXStartLocation--;
            tileNameX = tilemap.GetSprite(new Vector3Int(wheatXStartLocation, 0, 0)).name;
        }
        wheatXStartLocation++;

        // Find wheat's y start location
        var tileNameY = "dirt";
        var wheatYLocation = 0;
        while (tileNameY.Equals("dirt")) {
            wheatYLocation--;
            tileNameY = tilemap.GetSprite(new Vector3Int(0, wheatYLocation, 0)).name;
        }
        wheatYLocation++;

        var wheatSize = wheatPrefab.GetComponent<SpriteRenderer>().bounds.size;
        var wheatXLocation = wheatXStartLocation;

        while (tilemap.GetSprite(new Vector3Int(0, wheatYLocation, 0)).name.Equals("dirt")) {
            while (tilemap.GetSprite(new Vector3Int(wheatXLocation, 0, 0)).name.Equals("dirt")) {
                Instantiate(wheatPrefab, new Vector3(wheatXLocation + wheatSize.x / 2.0f, wheatYLocation + wheatSize.y / 2.0f, 0.0f), Quaternion.identity);
                wheatXLocation++;
            }
            wheatXLocation = wheatXStartLocation;
            wheatYLocation++;
        }
    }
}
