using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.Play("Theme");
        SoundManager.instance.Play("Ambiente");

        Race.Instance.OnStartRace += () => { SoundManager.instance.Play("Clock"); SoundManager.instance.Play("StartRace"); };
        Race.Instance.OnDificultiUp += () => SoundManager.instance.GetSound("Clock").source.pitch += 0.2f;
        Race.Instance.OnEndRace += () => { SoundManager.instance.Play("Death"); SoundManager.instance.Stop("Clock"); };

    }
    
}
