using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject cameraCurrent;  //камера в кадре
    private PlayerMovement _playerMovement;  //если персонаж умер
    
    private void Start()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)  //при переходе в кадр включаем камеру
    {
        if (other.tag == "Player")
        {
            cameraCurrent.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)  //при выходе из кадра выключаем камеру
    {
        if (other.tag == "Player" && Camera.allCamerasCount > 1 && !_playerMovement.isDead)
        {
            cameraCurrent.SetActive(false);
        }
    }
}
