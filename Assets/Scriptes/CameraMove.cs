using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject cameraCurrent;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            cameraCurrent.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && Camera.allCamerasCount > 1 && !_playerMovement.isDead)
        {
            cameraCurrent.SetActive(false);
        }
    }
}
