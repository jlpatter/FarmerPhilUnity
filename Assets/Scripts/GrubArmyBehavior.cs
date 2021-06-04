using UnityEngine;

public class GrubArmyBehavior : MonoBehaviour {
    public int NumOfGrubs { get; set; }
    public int NumOfGrubsForWave { get; set; }

    private void Start() {
        NumOfGrubsForWave = 4;
        NumOfGrubs = NumOfGrubsForWave;
    }
}
