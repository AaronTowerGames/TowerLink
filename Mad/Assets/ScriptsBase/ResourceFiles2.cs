using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public enum TypeFile
{
    Texture,
    Sound
}

public class ResourceFiles2 : GSC<ResourceFiles2>
{
    public Dictionary<string, Texture> textureFiles = new Dictionary<string, Texture>();
    public Dictionary<string, Sprite> spriteFiles = new Dictionary<string, Sprite>();
    public Dictionary<string, AudioClip> audioFiles = new Dictionary<string, AudioClip>();

    public Texture GetTexture(string name)
    {
        if (textureFiles.ContainsKey(name))
        {
            return textureFiles[name];
        }
        else
        {
            var sprite = Resources.Load<Texture>(Path.Combine("Textures", name)); //name
            if (sprite != null)
            {
                textureFiles[name] = sprite;
                return textureFiles[name];
            }
            else
            {
                Texture texture = FileController2.GetTextureFromFile(name);
                if (texture == null)
                {
                    EventBus.ShowNotice.Invoke("FileNotFound");
                    return null;
                }
                else
                {
                    textureFiles[name] = texture;
                    return texture;
                }
            }
        }
    }

    public Sprite GetSprite(string name)
    {
        if (spriteFiles.ContainsKey(name))
        {
            return spriteFiles[name];
        }
        else
        {
            var sprite = Resources.Load<Sprite>(Path.Combine("Sprites", name)); //name
            if (sprite != null)
            {
                spriteFiles[name] = sprite;
                return spriteFiles[name];
            }
            else
            {
                Sprite texture = FileController2.GetSprite(name);
                if (texture == null)
                {
                    EventBus.ShowNotice.Invoke("FileNotFound");
                    return null;
                }
                else
                {
                    spriteFiles[name] = texture;
                    return texture;
                }
            }
        }
    }

    public AudioClip GetAudioClip(string name)
    {
        if (audioFiles.ContainsKey(name))
        {
            return audioFiles[name];
        }
        else
        {
            var audioClip = Resources.Load<AudioClip>(Path.Combine("Sounds", name)); //name
            if (audioClip != null)
            {
                audioFiles[name] = audioClip;
                return audioFiles[name];
            }
            else
            {
                if (FileController2.IsFileExist(name + DataSettings.DEFAULT_EXT_SOUND))
                {
                    EventBus.GetAudio.Invoke(name);                    
                    return null;
                }
                else
                {
                    EventBus.ShowNotice.Invoke("FileNotFound");
                    return null;
                }
            }
        }
    }
}