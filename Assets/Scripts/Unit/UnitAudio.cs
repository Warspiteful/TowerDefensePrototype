using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class UnitAudio : MonoBehaviour
{
    private EventInstance _soundInstance;

    private EventReference _attackSound;
    
    public void Initialize()
    {
        
    }

    public void PlayAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(_attackSound);
    }
}
