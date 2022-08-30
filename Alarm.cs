using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    private float _speedVolumeChange = 0.001f;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine _volume;

    private void Start()
    {
        StartVolumeUp();
        StartVolumeDown();
    }
    
    private IEnumerator VolumeChange(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedVolumeChange);
            yield return null;
        }
    }
    
    public void StartVolumeUp()
    {
        _audioSource.Play();
        if (_volume != null)
        {
            StopCoroutine(_volume);
        }
        _volume = StartCoroutine(VolumeChange(_maxVolume));
    }

    public void StartVolumeDown()
    {
        if (_volume != null)
        {
            StopCoroutine(_volume);
        }
        _volume = StartCoroutine(VolumeChange(_minVolume));
    }

}