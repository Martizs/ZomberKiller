using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField]
    Transform weapons;

    GameObject currentWeapon;
    public GameObject CurrentWeapon
    {
        get { return currentWeapon; }
    }

    AudioSource audioSource;

    int weaponIndex = 0;

    KeyCode[] weaponKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

    private void Start()
    {
        currentWeapon = weapons.GetChild(weaponIndex).gameObject;
        currentWeapon.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        int prevWeaponIndex = weaponIndex;
        ProcessScrollWheel();
        ProcessKeyInput();
        if (prevWeaponIndex != weaponIndex)
        {
            audioSource.Play();
        }
    }

    void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponIndex = 0;
            SetWeaponActive();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponIndex = 1;
            SetWeaponActive();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponIndex = 2;
            SetWeaponActive();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponIndex = 3;
            SetWeaponActive();
        }
    }

    void ProcessScrollWheel()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            ChangeWeaponIndex(weaponIndex - 1);
            SetWeaponActive();
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            ChangeWeaponIndex(weaponIndex + 1);
            SetWeaponActive();
        }
    }

    void ChangeWeaponIndex(int newIndex)
    {
        if (newIndex < 0)
        {
            weaponIndex = weapons.childCount - 1;
        }
        else if (newIndex >= weapons.childCount)
        {
            weaponIndex = 0;
        }
        else
        {
            weaponIndex = newIndex;
        }
    }

    void SetWeaponActive()
    {
        currentWeapon.SetActive(false);
        currentWeapon = weapons.GetChild(weaponIndex).gameObject;
        currentWeapon.SetActive(true);
    }
}
