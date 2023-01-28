using System.IO;
using UnityEngine;

public class AudioUI : GSC<AudioUI>
{
    [SerializeField]
    private AudioSource audioSource;

    private AudioClip audioClip;

    public void Play(AudioClip _audioClip)
    {
        audioSource.PlayOneShot(_audioClip);

    }

    public void PlaySound(string _audioClipName)
    {
        var audio = ResourceFiles2.Instance.GetAudioClip(_audioClipName);
        if (audio == null)
        {
            PlaySoundFromResources(_audioClipName);
        }
        else
        {
            audioClip = audio;
            audioSource.PlayOneShot(audioClip);
        }        
    }

    private void PlaySoundFromResources(string _audioClipName)
    {
        var s = _audioClipName;
        
        audioClip = Resources.Load<AudioClip>(Path.Combine("Sounds", s));
        if (audioClip == null)
        {
            return;
        }
        else
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public AudioClip GetSoundFromResources(string _audioClipName)
    {
        var s = _audioClipName;

        audioClip = Resources.Load<AudioClip>(Path.Combine("Sounds", s));
        if (audioClip == null)
        {
            return null;
        }
        else
        {
            return audioClip;
        }
    }
}
