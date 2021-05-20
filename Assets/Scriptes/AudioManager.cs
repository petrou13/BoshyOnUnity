using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;  //аудиомиксер
    public Slider musicSlider, sfxSlider;  //слайдеры громкости музыки и звуков
    public float musicFloat, sfxFloat;  //значения громкости музыки и звуков

    [SerializeField] private AudioClip deathMusic;  //музыка при смерти
    [SerializeField] private AudioSource audioSource;  //источник музыки
    private int firstPlayInt;  //это первый запуск?
    private bool isPaused;  //игра на паузе

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt("FirstPlay");
        audioSource = GetComponent<AudioSource>();

        if (LoadAudioSettings() != null)  //если есть настройки аудио
        {
            AudioData data = LoadAudioSettings();

            musicFloat = data.musicVol;
            sfxFloat = data.sfxVol;

            musicSlider.value = data.musicVol;
            sfxSlider.value = data.sfxVol;

            mixer.SetFloat("MusicVol", data.musicVol);
            mixer.SetFloat("SFXVol", data.sfxVol);

            PlayerPrefs.SetFloat("MusicVol", data.musicVol);
            PlayerPrefs.SetFloat("SFXVol", data.sfxVol);
            PlayerPrefs.SetInt("FirstPlay", -1);
        }
        else if (firstPlayInt == 0)  //если первый раз зашел
        {
            musicFloat = musicSlider.value;
            sfxFloat = sfxSlider.value;

            PlayerPrefs.SetFloat("MusicVol", musicFloat);
            PlayerPrefs.SetFloat("SFXVol", sfxFloat);
            PlayerPrefs.SetInt("FirstPlay", -1);
        }
        else
        {
            musicFloat = PlayerPrefs.GetFloat("MusicVol");
            musicSlider.value = musicFloat;
            mixer.SetFloat("MusicVol", musicFloat);

            sfxFloat = PlayerPrefs.GetFloat("SFXVol");
            sfxSlider.value = sfxFloat;
            mixer.SetFloat("SFXVol", sfxFloat);
        }

    }

    private void Update()
    {
        if (Time.timeScale == 0)  //пауза проигрывание во время паузы игры
        {
            audioSource.Pause();
            isPaused = true;
        }
        else if (Time.timeScale == 1 && isPaused)  //возобновление музыки
        {
            audioSource.Play();
            isPaused = false;
        }
    }

    public void PlayerDead()
    {
        audioSource.Stop();
        audioSource.clip = deathMusic;
        audioSource.Play();
    }

    public static void SaveSoundSettings(AudioManager manager)  //сохранение настроек аудио
    {
        PlayerPrefs.SetFloat("MusicVol", manager.musicFloat);
        PlayerPrefs.SetFloat("SFXVol", manager.sfxFloat);

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/audiosettings.lol";
        FileStream stream = new FileStream(path, FileMode.Create);

        AudioData data = new AudioData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static AudioData LoadAudioSettings()  //загрузка настроек аудио
    {
        string path = Application.persistentDataPath + "/audiosettings.lol";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AudioData data = formatter.Deserialize(stream) as AudioData;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("Audio settings file not found in " + path);
            return null;
        }
    }

    public void MusicVol(float musicValue)  //изменение значения громкости музыки
    {
        musicFloat = musicValue;
        mixer.SetFloat("MusicVol", musicValue);
        PlayerPrefs.SetFloat("MusicVol", musicValue);
        SaveSoundSettings(this);
    }
    public void SFXVol(float sfxValue)  //изменение значения громкости звуков
    {
        sfxFloat = sfxValue;
        mixer.SetFloat("SFXVol", sfxValue);
        PlayerPrefs.SetFloat("SFXVol", sfxValue);
        SaveSoundSettings(this);
    }

}
