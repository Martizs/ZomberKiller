using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EndGameHandler))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int hp = 5;

    [SerializeField]
    AudioClip itemPickupSound;

    AudioSource audioSource;

    public int HP
    {
        get { return hp; }
    }

    EndGameHandler deathHandler;

    private void Start()
    {
        deathHandler = FindObjectOfType<EndGameHandler>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            deathHandler.HandleEndGame();
        }
    }
}
