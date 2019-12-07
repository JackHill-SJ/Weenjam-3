using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
public class EnemyFollowing : MonoBehaviour
{
    [Header("Essentials")]
    public GameObject target;
    Vector3 destination;
    NavMeshAgent agent;
    Animator anim;
    [Header("Gore")]
    public GameObject Gore;
    public GameObject[] GoreSpawns;
    public GameObject GoreCenterSpawn;
    [Header("Status Bools")]
    private bool inRange = true;
    public bool isDead;
    [Header("Damage Realted")]
    [SerializeField] private float damage;
    [SerializeField] private bool attacking;
    [SerializeField] private Transform attackObject;
    [SerializeField] private bool hitSomething;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        UpdateMoveAnimations();
        //GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (target != null && inRange && !attacking)
        {
            agent.destination = target.transform.position;
        }
        if (target != null && Vector3.Distance(transform.position, target.transform.position) < 2f && !attacking)
        {
            anim.SetTrigger("Attack");
            //StartCoroutine("SwingAtAir");
        }
    }

    public void Attack()
    {
        RaycastHit hit;
        hitSomething = Physics.BoxCast(transform.position, transform.localScale, attackObject.forward, out hit, Quaternion.identity, 1.7f);
        if (hitSomething)
        {
            if (hit.collider.tag == "Player")
            {
                print("Hit Player");
                hit.collider.gameObject.GetComponent<PlayerHealth>().Hurt(damage);
            }
        }
        else
        {
            print("Nothing was hit");
        }
            
    }

    public void SpawnGore()
    {
        /*foreach(GameObject spawner in GoreSpawns)
        {
            GameObject flesh = Instantiate(Gore, GoreCenterSpawn.transform.position, GoreCenterSpawn.transform.rotation);
            flesh.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-300, 300), Random.Range(-500, 500), Random.Range(-300, 300)));
            flesh.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-500, 500), Random.Range(-500, 500), Random.Range(-500, 500)));
            float scale = Random.Range(0.5f, 1f);
            flesh.transform.localScale = new Vector3(scale, scale ,scale);
        }*/
        //GameObject flesh = Instantiate(Gore, GoreCenterSpawn.transform.position, GoreCenterSpawn.transform.rotation);
    }

    private void OnDestroy()
    {
    }

    void UpdateMoveAnimations()
    {
        anim.SetTrigger("PlayerRange");
    }

    /*void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            UpdateMoveAnimations();
            inRange = true;
            agent.stoppingDistance = 2;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UpdateMoveAnimations();
            inRange = false;
            agent.SetDestination(transform.position);
        }
    }*/

    /*IEnumerator SwingAtAir()
    {
        if (!attacking)
        {
            attacking = true;
            RaycastHit hit;
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.29f);
            Physics.BoxCast(transform.position, Vector3.one, attackObject.forward, out hit);
            if (hit.collider.tag == "Player")
            {
                print("Hit Player");
                hit.collider.gameObject.GetComponent<PlayerHealth>().Hurt(damage);
            }
            attacking = false;
        }
    }*/

}
