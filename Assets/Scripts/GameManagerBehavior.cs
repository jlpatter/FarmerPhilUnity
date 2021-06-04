using UnityEngine;

public class GameManagerBehavior : MonoBehaviour {
    public int CurrentWave { get; set; }

    private void Start() {
        CurrentWave = 1;
    }
}
