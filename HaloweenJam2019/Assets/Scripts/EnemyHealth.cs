using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth = 2;
    float totalHealth;
    public GameObject soundSource;
    public EnemyCounter enemyCounter;

    private void Start()
    {
        totalHealth = currentHealth;
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        print("I was hit for: " + damageAmount);
        if(currentHealth <= 0 && !GetComponent<EnemyFollowing>().isDead)
        {
            //GetComponent<EnemyFollowing>().SpawnGore();
            //GetComponent<EnemyFollowing>().SpawnGore();
            GetComponent<EnemyFollowing>().isDead = true;
            if (soundSource)
            {
                Instantiate(soundSource, transform.position, Quaternion.identity);
            }
            enemyCounter.zombieCount -= 1;
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            Damage(9999f);
        }

    }*/
}
