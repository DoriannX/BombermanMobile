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

    public void PlayButtonSound1()
    {
        PlayAtPath("ButtonClick1", 1, 0.05f);
    }
    
    public void PlayButtonSound2()
    {
        PlayAtPath("ButtonClick2", 1);
    }

    public void PlayAtPath(string path, float volume = 1, float randomness = 0.15f)
    {
        AudioClip newClip = Resources.Load<AudioClip>(path);
        CustomSound sound = new CustomSound();
        sound.AudioClip = newClip;
        sound.b_RandomPitch = true;
        sound.MaxPitchRandomness = randomness;
        sound.Volume = volume;
        Play(sound);
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
