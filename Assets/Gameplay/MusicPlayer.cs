using JHiga.RTSEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource source;
    public float fadeTime;
    public float maxVolume;
    void Start()
    {
        elapsedTime = 0;
        source = GetComponent<AudioSource>();
        source.volume = 0;
        source.clip = PlayerContext.players[PlayerContext.PlayerId].faction.backgroundMusic;
        source.Play();
        StartCoroutine(VolumeIncrease());
        
    }
    private float elapsedTime;
    private IEnumerator VolumeIncrease()
    {
        while (elapsedTime < fadeTime)
        {
            source.volume = Mathf.Lerp(0, maxVolume, elapsedTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
