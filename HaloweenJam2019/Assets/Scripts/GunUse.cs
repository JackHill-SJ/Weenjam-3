using UnityEngine;
using System.Collections;

public class GunUse : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject bullets;
    public GameObject player;
    public Transform gunEnd;
    public Animator childAnimator;
    public GameObject flash;
    public GameObject ammoUI;
    public Sprite[] ammoNumber;
    private AudioSource[] clips;
    private Transform cam;
    [Header("Math Stuff")]
    public int gunDamage;
    public float fireRate;
    public int bulletCount;
    public float bulletSpread;
    private int bulletPerMagazine = 6;
    private int currentAmmo = 6;
    private Vector3 vectorRef = new Vector3(0, 0, 0);
    [Header("Bools")]
    public bool isShooting = false;
    public bool isReloading = false;
    public bool camKick = false;
    public bool endlessAmmo;
    [Header("Recoil Smoothing")]
    public int smoothFrames;
    public float maxRecoil;

    void Start()
    {
        clips = GetComponentsInChildren<AudioSource>();
        cam = transform.parent.transform;//.parent.transform;
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            StartCoroutine("Shoot");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("Reload");
        }
    }
    /*
    IEnumerator CamKick()
    {
        float totalRot = 0;
        float recoilIncrement = -(maxRecoil / smoothFrames);
        for(int i = smoothFrames; i > 0; i--)
        {
            print("Looping");
            totalRot += recoilIncrement;
            cam.Rotate(new Vector3(totalRot, 0, 0));
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = smoothFrames; i > 0; i--)
        {
            yield return new WaitForFixedUpdate();
            cam.Rotate(new Vector3(-recoilIncrement, 0, 0));
        }



    }*/
    IEnumerator Shoot()
    {
        if (!isShooting && !isReloading)
        {
            if (currentAmmo > 0 || endlessAmmo)
            {
                isShooting = true;
                StartCoroutine("Flash");
                for (int i = 0; i < bulletCount; i += 1)
                {
                    GameObject temp = Instantiate(bullets, gunEnd.transform.position, gunEnd.transform.rotation * Quaternion.Euler(90 + Random.Range(-bulletSpread / 2, bulletSpread / 2), 0, Random.Range(-bulletSpread / 2, bulletSpread / 2)));
                }
                childAnimator.SetTrigger("Shoot");
                currentAmmo -= 1;
                if(ammoUI)
                    ammoUI.GetComponent<UnityEngine.UI.Image>().sprite = ammoNumber[currentAmmo];
                if(clips.Length > 0)
                    clips[0].Play();
                //StartCoroutine("CamKick");
                yield return new WaitForSeconds(fireRate);
                isShooting = false;
            }
            else
            {
                //outta ammo
            }
        }
    }
    IEnumerator Reload()
    {
        if (!isReloading && !isShooting)
        {
            isReloading = true;

            childAnimator.SetTrigger("Reload");


            yield return new WaitForSeconds(.35f);
            clips[1].Play();
            yield return new WaitForSeconds(1.35f);
            clips[2].Play();
            yield return new WaitForSeconds(.6f);

            currentAmmo = bulletPerMagazine;
            ammoUI.GetComponent<UnityEngine.UI.Image>().sprite = ammoNumber[currentAmmo];
            isReloading = false;
        }
    }
    IEnumerator Flash()
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
    }
}