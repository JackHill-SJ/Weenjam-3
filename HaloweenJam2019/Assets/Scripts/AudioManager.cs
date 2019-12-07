using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(DoClips());
    }
    IEnumerator DoClips()
    {
        yield return new WaitForSeconds(Random.Range(10, 25));
        audioSource.PlayOneShot(audioClips[(int)Random.Range(0, audioClips.Length - 1)]);
        StartCoroutine(DoClips());
    }
}