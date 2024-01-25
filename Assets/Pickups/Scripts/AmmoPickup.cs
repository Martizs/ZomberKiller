using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField]
    int ammoReward = 5;

    [SerializeField]
    AmmoType ammoType;

    Ammo ammo;

    ItemPickuper itemPickuper;

    private void Start()
    {
        ammo = FindObjectOfType<Ammo>();
        itemPickuper = FindObjectOfType<ItemPickuper>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            itemPickuper.PlayItemPickupSound();
            ammo.IncreaseCurrentAmmo(ammoType, ammoReward);
            Destroy(gameObject);
        }
    }
}
