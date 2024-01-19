using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    TextMeshProUGUI equipmentText;
    WeaponSwitcher weaponSwitcher;
    PlayerHealth playerHealth;
    Flashlight flashlight;
    Ammo ammo;

    [SerializeField]
    Transform zombers;

    int zombersLeft;

    // Start is called before the first frame update
    void Start()
    {
        equipmentText = GetComponent<TextMeshProUGUI>();
        ammo = FindObjectOfType<Ammo>();
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        flashlight = FindObjectOfType<Flashlight>();
        zombersLeft = zombers.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    public void DecreaseZomberAmount()
    {
        zombersLeft--;
        if (zombersLeft <= 0)
        {
            Invoke("WinScreen", 2f);
        }
    }

    void WinScreen()
    {
        FindObjectOfType<EndGameHandler>().HandleEndGame(false);
    }

    void UpdateText()
    {
        equipmentText.text =
            $"Bullets: {ammo.GetCurrentAmmo(AmmoType.Bullets)}; Shells: {ammo.GetCurrentAmmo(AmmoType.Shells)}; SniperBullets: {ammo.GetCurrentAmmo(AmmoType.HighCalliberBullets)}; PeaBullets: {ammo.GetCurrentAmmo(AmmoType.PeaBullets)};\nCurrent weapon: {weaponSwitcher.CurrentWeapon.name}\nHP: {playerHealth.HP}\nLight: {flashlight.GetLightIntensity()}\nZombers left: {zombersLeft}";
    }
}
