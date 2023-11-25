using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class GameSettingSctrip : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SettingVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
