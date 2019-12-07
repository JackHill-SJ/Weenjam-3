using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage;
    public bool Attacking;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("attack", 0.01f, 3.0f);
        }
        else
        {
            CancelInvoke("attack");
        }
    }

    private void attack()
    {
        {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Hurt(attackDamage);
            }
        }
    }
}
