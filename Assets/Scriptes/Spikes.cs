using UnityEngine;

public class Spikes : MonoBehaviour
{
    private PlayerMovement player;
    private float originalY;  //изначальная позиция по Y
    [SerializeField] private float floatStrength = 0.01f;  //высота полета
    [SerializeField] private int timeScale = 5;  //ускорение полета
    [SerializeField] private bool isMoving = false;  //оно двигается
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

    private void Update()
    {
        if(isMoving)
        {
            transform.position = new Vector2(transform.position.x, originalY + (Mathf.Sin(Time.time * timeScale) * floatStrength));
        }
    }
}
