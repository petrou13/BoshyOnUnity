using UnityEngine;

public class CheckpointBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;  //скорость полета пули
    private GameObject player;  //позиция игрока
    private Rigidbody2D body;  //тело пули
    Vector2 wasHere;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        body.velocity = transform.right * bulletSpeed;  //передвижение пули
    }

    void OnTriggerEnter2D(Collider2D hitInfo)   //уничтожение пули
    {
        if (hitInfo.tag != "Confiner" && hitInfo.tag != "NPC" && hitInfo.tag != "JumpBuster" && hitInfo.tag != "Edge" && hitInfo.tag != "Checkpoint")
        {
            Destroy(gameObject);
        }
        if(hitInfo.name == "Player")
        {
            Destroy(gameObject);
            player.GetComponent<PlayerMovement>().Dead();
        }
    }
}
