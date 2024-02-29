using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject Play(CustomSound newCustomSound)
    {
        GameObject newObject = new GameObject();
        AudioSource audioSource = newObject.AddComponent<AudioSource>();
        audioSource.clip = newCustomSound.AudioClip;
        audioSource.loop = newCustomSound.b_IsLoop;
        audioSource.volume = newCustomSound.Volume;
        if (newCustomSound.b_IsLocated)
        {
            audioSource.maxDistance = newCustomSound.MaxDistance;
        }
        else
        {
            audioSource.maxDistance = 9999999999;
        }
        if (newCustomSound.b_RandomPitch)
        {
            audioSource.pitch = 1 + Random.Range(-newCustomSound.MaxPitchRandomness, newCustomSound.MaxPitchRandomness);
        }
        audioSource.playOnAwake = true;
        return Instantiate(newObject, newCustomSound.AudioPosition, Quaternion.identity).gameObject;
    }
}
