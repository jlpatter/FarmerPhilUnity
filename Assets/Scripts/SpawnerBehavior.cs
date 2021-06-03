using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnerBehavior : MonoBehaviour {
    public GameObject wheatPrefab;
    public GameObject grubPrefab;
    public Tilemap tilemap;

    private GameObject _wheatField;
    private GameObject _grubArmy;
    private int _numOfGrubs;

    // Start is called before the first frame update
    private void Start() {
        _wheatField = GameObject.Find("WheatField");
        _grubArmy = GameObject.Find("GrubArmy");
        _numOfGrubs = 4;
        
        SpawnWheat();
        SpawnGrubs();
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

    private void SpawnGrubs() {
        var stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        for (var i = 0; i < _numOfGrubs; i++) {
            var xRandom = 0;
            var yRandom = 0;
            // Don't allow both to be 0 as that would put the grub in the center of the field.
            while (xRandom == 0 && yRandom == 0) {
                xRandom = Random.Range(-1, 2);
                yRandom = Random.Range(-1, 2);
            }
            Instantiate(grubPrefab, new Vector3(stageDimensions.x * xRandom, stageDimensions.y * yRandom, 0.0f), Quaternion.identity, _grubArmy.transform);
        }
    }
}
