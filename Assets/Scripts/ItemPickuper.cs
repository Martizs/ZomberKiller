using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickuper : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayItemPickupSound()
    {
        audioSource.Play();
    }
}
