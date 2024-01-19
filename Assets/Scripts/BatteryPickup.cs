using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    Flashlight flashlight;

    ItemPickuper itemPickuper;

    private void Start()
    {
        flashlight = FindObjectOfType<Flashlight>();
        itemPickuper = FindObjectOfType<ItemPickuper>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            itemPickuper.PlayItemPickupSound();
            flashlight.RestoreLightIntensity();
            flashlight.RestoreLightAngle();
            Destroy(gameObject);
        }
    }
}
