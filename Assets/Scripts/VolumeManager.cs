using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeManager : MonoBehaviour
{
    public VolumeSO volumeSO;
    public Slider BGMVol, SFXVol, MasterVol;
    public List<AudioMixerGroup> mixerGroups;
    private void Awake()
    {
        BGMVol.value = volumeSO.musicGroupVolume;
        SFXVol.value = volumeSO.sfxGroupVolume;
        MasterVol.value = volumeSO.mainGroupVolume;
    }
    public void OnBGMValueChanged()
    {
        volumeSO.musicGroupVolume = BGMVol.value;
       
    }
    public void OnSFXValueChanged()
    {
        volumeSO.sfxGroupVolume = SFXVol.value;
       
    }
    public void OnMainValueChanged()
    {
        volumeSO.mainGroupVolume = MasterVol.value;
    }
}
