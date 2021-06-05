using System;
using System.Collections.Generic;
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
        RemoveWeapons();
        _shopMenu.SetIsShop(true);
    }

    private void RemoveWeapons() {
        _playerBehavior.CurrentWeaponIndex = 0;
        _playerBehavior.currentWeaponText.text = _playerBehavior.Weapons[_playerBehavior.CurrentWeaponIndex].Item1.tag;
        var weaponsToRemove = new List<Tuple<GameObject, SpriteRenderer>>();
        foreach (var weaponTuple in _playerBehavior.Weapons) {
            if (!weaponTuple.Item1.name.Contains("Bat")) {
                foreach (Transform child in _playerBehavior.gameObject.transform) {
                    if (child.gameObject.name.Contains(weaponTuple.Item1.name)) {
                        Destroy(child.gameObject);
                    }
                }
                weaponsToRemove.Add(weaponTuple);
            }
        }

        foreach (var weaponToRemove in weaponsToRemove) {
            _playerBehavior.Weapons.Remove(weaponToRemove);
        }
    }
}
