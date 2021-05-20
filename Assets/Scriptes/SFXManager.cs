using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private static AudioSource audioSource;  //источник музыки
    private static AudioClip playerJump, playerDoubleJump, playerShoot;  //звуки игрока
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerJump = Resources.Load<AudioClip>("SFX/Jump1");
        playerDoubleJump = Resources.Load<AudioClip>("SFX/Jump2");
        playerShoot = Resources.Load<AudioClip>("SFX/Fire");
    }


    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump1":
                {
                    audioSource.PlayOneShot(playerJump);
                    break;
                }
            case "jump2":
                {
                    audioSource.PlayOneShot(playerDoubleJump);
                    break;
                }
            case "shoot":
                {
                    audioSource.PlayOneShot(playerShoot);
                    break;
                }
        }
    }
}
