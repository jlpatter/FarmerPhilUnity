using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnerBehavior : MonoBehaviour {
    public GameObject wheatPrefab;
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start() {
        Instantiate(wheatPrefab, Vector3.zero, Quaternion.identity);
        Debug.Log(tilemap.GetSprite(new Vector3Int(0, 0, 0)).name);
        var tileNameX = "dirt";
        var x = 0;
        while (tileNameX.Equals("dirt")) {
            x--;
            tileNameX = tilemap.GetSprite(new Vector3Int(x, 0, 0)).name;
        }
        x++;

        var tileNameY = "dirt";
        var y = 0;
        while (tileNameY.Equals("dirt")) {
            y--;
            tileNameY = tilemap.GetSprite(new Vector3Int(0, y, 0)).name;
        }
        y++;

        Instantiate(wheatPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
    }
}
