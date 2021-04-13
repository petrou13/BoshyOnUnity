using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameManager gameManager;
    GameObject player;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            gameManager.savedPosition = player.transform.position;  
        }
    }
}
