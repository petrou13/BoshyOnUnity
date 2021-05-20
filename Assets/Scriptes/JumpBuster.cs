using UnityEngine;

public class JumpBuster : MonoBehaviour
{
    public float respawnTime = 1f;

    private PlayerMovement player;  //игрок
    private float originalY;  //изначальная позиция по Y
    [SerializeField] private float floatStrength = 0.01f;  //высота полета
    [SerializeField] private int timeScale = 5;  //ускорение полета

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();  //поиск игрока
        originalY = transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  //возможность прыгнуть еще один раз
        {
            if (player.curJumps == 2)
            {
                player.curJumps--;
            }
            gameObject.SetActive(false);

            Invoke("RespawnBuster", respawnTime);
        }
    }

    void RespawnBuster()  //респавн бустера
    {
        gameObject.SetActive(true);
    }

    private void Update()  //передвижение вверх-вниз
    {
        transform.position = new Vector2(transform.position.x, originalY + (Mathf.Sin(Time.time * timeScale) * floatStrength));
    }
}
