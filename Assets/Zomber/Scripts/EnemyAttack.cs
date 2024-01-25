using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;

    [SerializeField]
    int damage = 1;

    private void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    // This also gets called by broadcastmessage
    // public void OnDamageTaken()
    // {
    //     Debug.Log("I took damage");
    // }

    public void AttackHitEvent()
    {
        if (target != null)
        {
            target.TakeDamage(damage);
            target.GetComponent<DisplayDamage>().ShowDamageImpact();
        }
    }
}
