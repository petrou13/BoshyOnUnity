[System.Serializable]
public class AudioData
{
    public float musicVol, sfxVol;  //громкость звуков

    public AudioData(AudioManager manager)  //информация громкости
    { 
        musicVol = manager.musicFloat;
        sfxVol = manager.sfxFloat;
    }
}