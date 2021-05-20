using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    GameManager gameManager;  //сохранение
    GameObject player;  //игрок
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)  //при попадании в чекпоинт, происходит сохранение текущей позиции и сцены игрока
    {
        if(other.tag == "Bullet")
        {
            gameManager.savedPosition = player.transform.position;  
            gameManager.scene = SceneManager.GetActiveScene().buildIndex;
            SaveSystem.SavePlayer(gameManager);
        }
    }
}
