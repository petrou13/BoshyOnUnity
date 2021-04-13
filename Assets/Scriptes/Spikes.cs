using UnityEngine;

public class Spikes : MonoBehaviour
{
    private PlayerMovement player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  //смерть игрока
        {
            player.Dead();
        }
    }
}
