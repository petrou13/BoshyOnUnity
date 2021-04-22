using UnityEngine;

public class JumpBuster : MonoBehaviour
{
    private PlayerMovement player;  //игрок
    public float respawnTime = 1f;
    public Object busterRef;
    void Start()
    {
        //busterRef = Resources.Load("JumpBuster");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();  //поиск игрока
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

        // GameObject busterClone = (GameObject)Instantiate(busterRef);
        // busterClone.transform.position = transform.position;
        // busterClone.SetActive(true);
        // Destroy(gameObject);
    }
}
