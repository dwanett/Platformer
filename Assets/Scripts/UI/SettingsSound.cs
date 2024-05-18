using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SettingsSound : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _audioSourceBackground;
    [SerializeField] private AudioSource _audioSourceButton;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private float _timePlayClipButton;
    [SerializeField] private string _nameValueVolumeMaster;
    [SerializeField] private string _nameValueVolumeBackground;
    [SerializeField] private string _nameValueVolumeButton;
    [SerializeField] private float _valueVolumeOffSound;
    
    private float _saveVolume;
    public event Action<bool> EnabledSound;
    
    public bool IsEnableSound { get; private set; }
    
    private void Awake()
    {
        if (_audioSourceBackground.clip == null && _audioClips.Length > 0)
        {
            _audioSourceBackground.clip = _audioClips[0];
            _audioSourceBackground.Play();
        }
        
        _audioMixer.GetFloat(_nameValueVolumeMaster, out _saveVolume);
        IsEnableSound = _saveVolume > _valueVolumeOffSound;
        EnabledSound?.Invoke(IsEnableSound);
    }

    private IEnumerator TimePlaySound(AudioSource audioSource)
    {
        yield return new WaitForSeconds(_timePlayClipButton);
        audioSource.Stop();
    }
    
    public void PlayForTypeMusic(int numberClip)
    {
        _audioSourceButton.clip = _audioClips[numberClip];
        _audioSourceButton.Play();
        StartCoroutine(TimePlaySound(_audioSourceButton));
    }
    
    public void SetVolumeMaster(float volume)
    {
        _audioMixer.SetFloat(_nameValueVolumeMaster, Mathf.Log10(volume) * 20);
        _saveVolume = volume;
        IsEnableSound = _saveVolume > _valueVolumeOffSound;
        EnabledSound?.Invoke(IsEnableSound);
    }
    
    public void SetVolumeBackground(float volume)
    {
        _audioMixer.SetFloat(_nameValueVolumeBackground, Mathf.Log10(volume) * 20);
    }
    
    public void SetVolumeButton(float volume)
    {
        _audioMixer.SetFloat(_nameValueVolumeButton, Mathf.Log10(volume) * 20);
    }
    
    public void EnableAndDisableSound()
    {
        if (IsEnableSound)
        {
            IsEnableSound = false;
            _audioMixer.GetFloat(_nameValueVolumeMaster, out _saveVolume);
            _audioMixer.SetFloat(_nameValueVolumeMaster, _valueVolumeOffSound);
        }
        else
        {
            IsEnableSound = true;
            _audioMixer.SetFloat(_nameValueVolumeMaster, _saveVolume);
        }
        
        EnabledSound?.Invoke(IsEnableSound);
    }
}
