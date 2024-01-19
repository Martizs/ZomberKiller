using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    Camera FPCamera;

    ParticleSystem muzzleFlashz;

    [SerializeField]
    GameObject hitEffect;

    [SerializeField]
    float range = 100f;

    [SerializeField]
    int damage = 1;

    [SerializeField]
    float shotDelay = 0f;

    [SerializeField]
    AmmoType ammoType;

    Ammo ammoSlot;

    [SerializeField]
    AudioClip emptyMagazine;

    AudioSource audioSource;

    EndGameHandler endGameHandler;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Start()
    {
        muzzleFlashz = GameObject
            .FindGameObjectWithTag("ShootyEffect")
            .GetComponent<ParticleSystem>();

        ammoSlot = FindObjectOfType<Ammo>();

        audioSource = GetComponent<AudioSource>();
        endGameHandler = FindObjectOfType<EndGameHandler>();
    }

    void Update()
    {
        if (!endGameHandler.GameOver && Input.GetButtonDown("Fire1"))
        {
            HandleWeaponTrigger();
        }
    }

    void HandleWeaponTrigger()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            if (canShoot)
            {
                canShoot = false;
                StartCoroutine(Shoot());
            }
        }
        else
        {
            audioSource.Stop();
            audioSource.PlayOneShot(emptyMagazine);
        }
    }

    IEnumerator Shoot()
    {
        audioSource.Stop();
        audioSource.Play();
        canShoot = false;
        ammoSlot.ReduceCurrentAmmo(ammoType);
        PlayMuzzleFlash();

        RaycastHit hit;
        if (
            Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)
        )
        {
            CreateHitImpact(hit);
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    void PlayMuzzleFlash()
    {
        muzzleFlashz.Play();
    }

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject newHitEffect = Instantiate(
            hitEffect,
            hit.point,
            Quaternion.LookRotation(hit.normal)
        );

        Destroy(newHitEffect, 1f);
    }
}
