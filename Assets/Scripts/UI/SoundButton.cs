using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SoundButton : MonoBehaviour
{
    [SerializeField] private SettingsSound _settingsSound;
    [SerializeField] private Sprite _spriteEnabledSound;
    [SerializeField] private Sprite _spriteDisabledSound;
    [SerializeField] private Image _image;
    
    private void OnEnable()
    {
        _settingsSound.DisabledOrEnabledSound += ChangeImageSound;
    }

    private void OnDisable()
    {
        _settingsSound.DisabledOrEnabledSound -= ChangeImageSound;
    }
    
    private void ChangeImageSound(bool isEnableSound)
    {
        _image.sprite = isEnableSound ? _spriteEnabledSound : _spriteDisabledSound;
    }
}
