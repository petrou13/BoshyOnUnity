using UnityEngine;

public class Conveer : MonoBehaviour
{
    private Rigidbody2D body;  //тело игрока
    public float giveSpeed = 1f;  //скорость, которая дается игроку через конвееры
    void Start()
    {
        body = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other) //изменение скорости игрока
    {
        body.velocity = Vector2.right * giveSpeed;
    }
}
