using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public static class FileController2
{
    public static void Delete(string filename)
    {
        if (String.IsNullOrEmpty(filename))
        {
            return;
        }
        if (File.Exists(Path.Combine(Application.persistentDataPath, filename)))
            File.Delete(Path.Combine(Application.persistentDataPath, filename));
    }

    public static bool IsFileExist(string filename)
    {
        if (String.IsNullOrEmpty(filename))
        {
            return false;
        }

        if (File.Exists(Path.Combine(Application.persistentDataPath, filename)))
            return true;

        return false;
    }
    public static void SaveBinData<T>(T _saveData, string filename)
    {
        using FileStream file = File.Create(Path.Combine(Application.persistentDataPath, filename));

        new BinaryFormatter().Serialize(file, _saveData);
    }

    public static T LoadBinData<T>(string filename)
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, filename)))
        {
            using FileStream file =
              File.Open(Path.Combine(Application.persistentDataPath, filename), FileMode.Open);
            T data = (T)new BinaryFormatter().Deserialize(file);
            return data;
        }
        else
        {
            return default(T);
        }
    }

    public static void SaveJsonData<T>(T _saveData, string filename)
    {

        var json = JsonUtility.ToJson(_saveData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, filename), json);
    }

    public static void SaveJsonsData<T>(T[] _saveData, string filename)
    {
        var json = JsonHelper.ToJson<T>(_saveData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, filename), json);
    }

    public static T[] LoadJsonsData<T>(string filename)
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, filename)))
        {
            T[] data = JsonHelper.FromJson<T>(File.ReadAllText(Path.Combine(Application.persistentDataPath, filename)));
            return data;
        }
        else
        {
            var filedata = Resources.Load<TextAsset>(Path.Combine("Datas", filename));
            if (filedata != null)
            {
                T[] data = JsonHelper.FromJson<T>(filedata.text);
                return data;
            }
                
            return null;
        }
    }

    public static T LoadJsonData<T>(string filename)
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, filename)))
        {
            T data = JsonUtility.FromJson<T>(File.ReadAllText(Path.Combine(Application.persistentDataPath, filename)));
            return data;
        }
        else
        {
            var filedata = Resources.Load<TextAsset>(Path.Combine("Datas", filename));
            if (filedata != null)
            {
                T data = JsonUtility.FromJson<T>(filedata.text);
                return data;
            }

            return default(T);
        }
    }

    public static Texture2D GetTextureFromFile(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;

        try
        {
            if (IsFileExist(name + DataSettings.DEFAULT_EXT_IMAGE))
            {
                byte[] bytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, name+DataSettings.DEFAULT_EXT_IMAGE));
                var width = bytes[16] << 24 | bytes[17] << 16 | bytes[18] << 8 | bytes[19];
                var height = bytes[20] << 24 | bytes[21] << 16 | bytes[22] << 8 | bytes[23];

                Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
                texture.filterMode = FilterMode.Trilinear;
                texture.LoadImage(bytes);
                return texture;
            }
            else
            {
                return null;
            }
        }
        catch //(IOException ex)
        {
            return null;
        }
    }

    public static Sprite GetSprite(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;

        try
        {
            if (IsFileExist(name+ DataSettings.DEFAULT_EXT_IMAGE))
            {
                var tex = GetTextureFromFile(Path.Combine(Application.persistentDataPath, name));
                Rect rec = new Rect(0, 0, tex.width, tex.height);
                return Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f), 100);
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    public static AudioClip GetSound(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;

        try
        {
            if (IsFileExist(name + DataSettings.DEFAULT_EXT_SOUND)) 
            {
                return ToAudioClip(Path.Combine(Application.persistentDataPath, name + DataSettings.DEFAULT_EXT_SOUND), name);
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    public static float[] ConvertByteToFloat(byte[] array)
    {
        float[] floatArr = new float[array.Length / 4];
        for (int i = 0; i < floatArr.Length; i++)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array, i * 4, 4);
            }
            floatArr[i] = BitConverter.ToSingle(array, i * 4) / 0x80000000;
        }
        return floatArr;
    }

    [Obsolete]
    public static IEnumerator GetConntentLength(string url, Action<int> response)
    {
        var request = UnityWebRequest.Head(url);
        yield return request.SendWebRequest();
        if (!request.isHttpError && !request.isNetworkError)
        {
            var contentLength = request.GetResponseHeader("Content-Length");

            if (int.TryParse(contentLength, out int returnValue))
            {
                response(returnValue);
            }
            else
            {
                response(-1);
            }
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);

            response(-1);
        }
    }

    //https://github.com/deadlyfingers/UnityWav/blob/master/WavUtility.cs
    public static AudioClip ToAudioClip(string filePath, string name)
    {
        if (!filePath.StartsWith(Application.persistentDataPath) && !filePath.StartsWith(Application.dataPath))
        {
            Debug.LogWarning("This only supports files that are stored using Unity's Application data path. \nTo load bundled resources use 'Resources.Load(\"filename\") typeof(AudioClip)' method. \nhttps://docs.unity3d.com/ScriptReference/Resources.Load.html");
            return null;
        }
        byte[] fileBytes = File.ReadAllBytes(filePath);
        return ToAudioClip(fileBytes, 0, name);
    }

    public static AudioClip ToAudioClip(byte[] fileBytes, int offsetSamples = 0, string name = "wav")
    {
        //string riff = Encoding.ASCII.GetString (fileBytes, 0, 4);
        //string wave = Encoding.ASCII.GetString (fileBytes, 8, 4);
        int subchunk1 = BitConverter.ToInt32(fileBytes, 16);
        UInt16 audioFormat = BitConverter.ToUInt16(fileBytes, 20);

        // NB: Only uncompressed PCM wav files are supported.
        string formatCode = FormatCode(audioFormat);
        Debug.AssertFormat(audioFormat == 1 || audioFormat == 65534, "Detected format code '{0}' {1}, but only PCM and WaveFormatExtensable uncompressed formats are currently supported.", audioFormat, formatCode);

        UInt16 channels = BitConverter.ToUInt16(fileBytes, 22);
        int sampleRate = BitConverter.ToInt32(fileBytes, 24);
        int byteRate = BitConverter.ToInt32 (fileBytes, 28);
        UInt16 blockAlign = BitConverter.ToUInt16 (fileBytes, 32);
        UInt16 bitDepth = BitConverter.ToUInt16(fileBytes, 34);

        int headerOffset = 16 + 4 + subchunk1 + 4;
        int subchunk2 = BitConverter.ToInt32(fileBytes, headerOffset);
        int wavSize = BitConverter.ToInt32(fileBytes, headerOffset);
        int numSamples = wavSize / (bitDepth / 8) / channels;

        float[] data;
        switch (bitDepth)
        {
            case 8:
                data = Convert8BitByteArrayToAudioClipData(fileBytes, headerOffset, subchunk2);
                break;
            case 16:
                data = Convert16BitByteArrayToAudioClipData(fileBytes, headerOffset, subchunk2);
                break;
            case 24:
                data = Convert24BitByteArrayToAudioClipData(fileBytes, headerOffset, subchunk2);
                break;
            case 32:
                data = Convert32BitByteArrayToAudioClipData(fileBytes, headerOffset, subchunk2);
                break;
            default:
                throw new Exception(bitDepth + " bit depth is not supported.");
        }

        //Debug.LogFormat("riff={0} wave={1} subchunk1={2} format={3} channels={4} sampleRate={5} byteRate={6} blockAlign={7} bitDepth={8} headerOffset={9} subchunk2={10} filesize={11}", numSamples, data.Length, subchunk1, formatCode, channels, sampleRate, byteRate, blockAlign, bitDepth, headerOffset, subchunk2, fileBytes.Length);
        //AudioClip audioClip = AudioClip.Create(name, data.Length, (int)channels, sampleRate, false);
        AudioClip audioClip = AudioClip.Create(name, numSamples, (int)channels, sampleRate, false);
        audioClip.SetData(data, 0);

        return audioClip;
    }

    //#region wav file bytes to Unity AudioClip conversion methods

    private static float[] Convert8BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
    {
        int wavSize = BitConverter.ToInt32(source, headerOffset);
        headerOffset += sizeof(int);
        Debug.AssertFormat(wavSize > 0 && wavSize == dataSize, "Failed to get valid 8-bit wav size: {0} from data bytes: {1} at offset: {2}", wavSize, dataSize, headerOffset);

        float[] data = new float[wavSize];

        sbyte maxValue = sbyte.MaxValue;

        int i = 0;
        while (i < wavSize)
        {
            data[i] = (float)source[i] / maxValue;
            ++i;
        }

        return data;
    }

    private static float[] Convert16BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
    {
        int wavSize = BitConverter.ToInt32(source, headerOffset);
        headerOffset += sizeof(int);
        int byteRate = BitConverter.ToInt32(source, 28);
        UInt16 bitDepth = BitConverter.ToUInt16(source, 34);
        UInt16 channels = BitConverter.ToUInt16(source, 22);
        Debug.AssertFormat(wavSize > 0 && wavSize == dataSize, "Failed to get valid 16-bit wav size: {0} from data bytes: {1} at offset: {2}", wavSize, dataSize, headerOffset);

        int x = sizeof(Int16); // block size = 2
        int convertedSize = wavSize / (bitDepth / 8) / channels;
        int convertedSize2 = (wavSize / x) / 2;

        //Debug.Log($"ConvertSize {convertedSize} {convertedSize2}");

        float[] data = new float[convertedSize];

        Int16 maxValue = Int16.MaxValue;

        int offset = 0;
        int i = 0;
        while (i < convertedSize)
        {
            offset = i * x + headerOffset;
            data[i] = (float)BitConverter.ToInt16(source, offset) / maxValue;
            ++i;
        }

        Debug.AssertFormat(data.Length == convertedSize, "AudioClip .wav data is wrong size: {0} == {1}", data.Length, convertedSize);

        // Определение позиции, на которой начинается пустота
        int nullBytePosition = -1;
        for (int j = data.Length - 1; j > 0 ; j--)
        {
            if (data[j] != 0)
            {
                nullBytePosition = j;
                break;
            }
        }

        if (nullBytePosition == -1 || nullBytePosition == data.Length - 1)
        {

        }
        else
        {
            // Определение количества байтов для удаления
            int bytesToDelete = 0;
            if (nullBytePosition >= 0)
            {
                for (int j = nullBytePosition; j < data.Length; j++)
                {
                    if (data[j] == 0)
                    {
                        bytesToDelete++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Создание нового массива данных и копирование в него байтов из исходного массива
            int newSize = data.Length - bytesToDelete;
            byte[] newData = new byte[newSize];
            Array.Copy(source, newData, newSize);

            return Convert16BitByteArrayToAudioClipData(source, headerOffset, newSize);
        }
        

        return data;
    }

    private static float[] Convert24BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
    {
        int wavSize = BitConverter.ToInt32(source, headerOffset);
        headerOffset += sizeof(int);
        //Debug.AssertFormat(wavSize > 0 && wavSize == dataSize, "Failed to get valid 24-bit wav size: {0} from data bytes: {1} at offset: {2}", wavSize, dataSize, headerOffset);

        int x = 3; // block size = 3
        int convertedSize = wavSize / x;

        int maxValue = Int32.MaxValue;

        float[] data = new float[convertedSize];

        byte[] block = new byte[sizeof(int)]; // using a 4 byte block for copying 3 bytes, then copy bytes with 1 offset

        int offset = 0;
        int i = 0;
        while (i < convertedSize)
        {
            offset = i * x + headerOffset;
            Buffer.BlockCopy(source, offset, block, 1, x);
            data[i] = (float)BitConverter.ToInt32(block, 0) / maxValue;
            ++i;
        }

        Debug.AssertFormat(data.Length == convertedSize, "AudioClip .wav data is wrong size: {0} == {1}", data.Length, convertedSize);

        return data;
    }

    private static float[] Convert32BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
    {
        int wavSize = BitConverter.ToInt32(source, headerOffset);
        headerOffset += sizeof(int);
        //Debug.AssertFormat(wavSize > 0 && wavSize == dataSize, "Failed to get valid 32-bit wav size: {0} from data bytes: {1} at offset: {2}", wavSize, dataSize, headerOffset);

        int x = sizeof(float); //  block size = 4
        int convertedSize = wavSize / x;

        Int32 maxValue = Int32.MaxValue;

        float[] data = new float[convertedSize];

        int offset = 0;
        int i = 0;
        while (i < convertedSize)
        {
            offset = i * x + headerOffset;
            data[i] = (float)BitConverter.ToInt32(source, offset) / maxValue;
            ++i;
        }

        Debug.AssertFormat(data.Length == convertedSize, "AudioClip .wav data is wrong size: {0} == {1}", data.Length, convertedSize);

        return data;
    }

    private static string FormatCode(UInt16 code)
    {
        switch (code)
        {
            case 1:
                return "PCM";
            case 2:
                return "ADPCM";
            case 3:
                return "IEEE";
            case 7:
                return "?-law";
            case 65534:
                return "WaveFormatExtensable";
            default:
                Debug.LogWarning("Unknown wav code format:" + code);
                return "";
        }
    }
}