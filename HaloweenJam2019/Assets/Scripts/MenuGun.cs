using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGun : MonoBehaviour
{
    public bool isReloading = false;
    public bool isShooting = false;
    public Animator childAnimator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("Reload");
        }
    }

    IEnumerator Reload()
    {
        if (!isReloading && !isShooting)
        {
            isReloading = true;

            childAnimator.SetTrigger("Reload");

            yield return new WaitForSeconds(2.3f);
            isReloading = false;
        }
    }
}
