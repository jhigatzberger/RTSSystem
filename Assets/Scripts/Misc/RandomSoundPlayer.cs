using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    AudioSource source;
    [SerializeField] private float initialMin, initialMax, standardMin, standardMax;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        Invoke(nameof(PlayAudio), Random.Range(initialMin, initialMax));
    }
    public void PlayAudio()
    {
        source.Play();
        Invoke(nameof(PlayAudio), Random.Range(standardMin, standardMax));
    }
}
