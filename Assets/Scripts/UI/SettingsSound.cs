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
    
    public event Action<bool> DisabledOrEnabledSound;
    
    private bool _isEnableSound;
    private float _saveVolume;
    
    private void Awake()
    {
        if (_audioSourceBackground.clip == null && _audioClips.Length > 0)
        {
            _audioSourceBackground.clip = _audioClips[0];
            _audioSourceBackground.Play();
        }
        
        _audioMixer.GetFloat(_nameValueVolumeMaster, out _saveVolume);
        _isEnableSound = _saveVolume > _valueVolumeOffSound;
        DisabledOrEnabledSound?.Invoke(_isEnableSound);
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
    
    public void VolumeMaster(float volume)
    {
        _audioMixer.SetFloat(_nameValueVolumeMaster, volume);
        _saveVolume = volume;
        _isEnableSound = _saveVolume > _valueVolumeOffSound;
        DisabledOrEnabledSound?.Invoke(_isEnableSound);
    }
    
    public void VolumeBackground(float volume)
    {
        _audioMixer.SetFloat(_nameValueVolumeBackground, volume);
    }
    
    public void VolumeButton(float volume)
    {
        _audioMixer.SetFloat(_nameValueVolumeButton, volume);
    }
    
    public void EnableAndDisableSound()
    {
        if (_isEnableSound)
        {
            _isEnableSound = false;
            _audioMixer.GetFloat(_nameValueVolumeMaster, out _saveVolume);
            _audioMixer.SetFloat(_nameValueVolumeMaster, _valueVolumeOffSound);
        }
        else
        {
            _isEnableSound = true;
            _audioMixer.SetFloat(_nameValueVolumeMaster, _saveVolume);
        }
        
        DisabledOrEnabledSound?.Invoke(_isEnableSound);
    }
}
