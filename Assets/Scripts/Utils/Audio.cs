using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Extensions
{
    public static IEnumerator FadeInCoroutine(this AudioSource audioSource)
    {
        float t = 0f;
        float volume = audioSource.volume;
        audioSource.volume = 0f;
        audioSource.Play();
        audioSource.loop = true;
        while (t < volume)
        {
            t += Time.deltaTime;
            audioSource.volume = t;
            yield return null;
        }
    }

    public static IEnumerator FadeOutCoroutine(this AudioSource audioSource)
    {
        float t = audioSource.volume;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            audioSource.volume = t;
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();
        audioSource.loop = false;
    }
}
