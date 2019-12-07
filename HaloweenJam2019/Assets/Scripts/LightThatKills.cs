using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightThatKills : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //print("Light Triggered");
        if (GetComponent<SphereCollider>().bounds.Contains(other.gameObject.transform.position))
        {
            if (other.gameObject.tag == "Enemy")
            {
                EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
                health.Damage(9999f);
            }
        } 
    }
}
