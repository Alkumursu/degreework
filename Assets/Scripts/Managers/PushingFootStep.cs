using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PushingFootStep : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClip;

    [SerializeField] AudioSource audioSource;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float spatialBlend;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Animation event
    private void PushStep()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        int index = Random.Range(0, audioClip.Length - 1);
        return audioClip[index];
    }
}
