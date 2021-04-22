using UnityEngine;

public class Conveer : MonoBehaviour
{
    private GameObject player;  //игрок
    private PlayerMovement facing;
    public float giveSpeed = 0.8f;  //скорость, которая дается игроку через конвееры
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        facing = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D other) //изменение скорости игрока
    {
        //body.velocity = Vector2.right * giveSpeed; ////не работает
        if (facing.facingRight)
        {
            player.transform.Translate(Vector2.right * (Time.deltaTime * giveSpeed));
        }
        else
        {
            player.transform.Translate(Vector2.left * (Time.deltaTime * giveSpeed));
        }
    }
}
