using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStuff : MonoBehaviour
{
    public float bulletDamage = 1;
    public float bulletSpeed = 50f;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up * bulletSpeed;
        Destroy(gameObject, 1f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<CapsuleCollider>().bounds.Contains(transform.position))
            {
                EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
                if (health != null)
                {
                    health.Damage(bulletDamage);
                }
            }
        }
    }
}