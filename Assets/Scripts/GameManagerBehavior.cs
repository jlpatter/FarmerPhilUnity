using Menus;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour {
    public int CurrentWave { get; set; }

    public PauseMenu pauseMenu;
    public Text moneyText;

    private ShopMenu _shopMenu;
    private PlayerBehavior _playerBehavior;
    private GameObject _pauseCanvas;
    private GameObject _wheatField;

    private const int PriceOfWheat = 1;

    private void Start() {
        CurrentWave = 1;
        _shopMenu = GameObject.Find("ShopMenuManager").GetComponent<ShopMenuManager>().shopMenu;
        _playerBehavior = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        _pauseCanvas = pauseMenu.gameObject.transform.parent.parent.gameObject;
        _wheatField = GameObject.Find("WheatField");
    }

    private void Update() {
        ShowPauseMenu();
    }

    private void ShowPauseMenu() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _pauseCanvas.SetActive(!_pauseCanvas.activeSelf);
            pauseMenu.IsPaused = !pauseMenu.IsPaused;
        }
    }

    public void EndWave() {
        _playerBehavior.Money += _wheatField.transform.childCount * PriceOfWheat;
        moneyText.text = "$" + _playerBehavior.Money;
        _shopMenu.SetIsShop(true);
    }
}
