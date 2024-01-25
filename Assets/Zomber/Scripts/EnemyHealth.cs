using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    int maxHp = 5;

    [SerializeField]
    int currHp = 0;
    public int CurrHp
    {
        get { return currHp; }
    }

    [SerializeField]
    AudioClip deadSound;

    [SerializeField]
    AudioClip hitSound;

    AudioSource audioSource;

    EquipmentUI equipmentUI;

    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;
        audioSource = GetComponent<AudioSource>();
        equipmentUI = FindObjectOfType<EquipmentUI>();
    }

    public bool IsDead()
    {
        return currHp <= 0;
    }

    public void TakeDamage(int damage)
    {
        if (!IsDead())
        {
            currHp -= damage;

            // GetComponent<EnemyAi>().OnDamageTaken();
            BroadcastMessage("OnDamageTaken");
            if (currHp <= 0)
            {
                equipmentUI.DecreaseZomberAmount();
                audioSource.Stop();
                audioSource.PlayOneShot(deadSound);
                GetComponent<Animator>().SetTrigger("die");
                // Destroy(gameObject);
            }
            else
            {
                audioSource.PlayOneShot(hitSound);
            }
        }
    }
}
