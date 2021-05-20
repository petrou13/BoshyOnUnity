using UnityEngine;

public class Pinguin : MonoBehaviour
{
    private float originalX;  //изначальная позиция по X
    private float originalY;  //изначальная позиция по Y
    [SerializeField] private float floatStrengthY = 0.02f;  //высота полета по оси Y
    [SerializeField] private float floatStrengthX = 0.015f;  //высота полета по оси X
    [SerializeField] private int timeScaleY = 30;  //ускорение полета по оси Y
    [SerializeField] private int timeScaleX = 20;  //ускорение полета по оси X
    private PlayerMovement _playerMovement;  //скрипт игрока    
    private GameObject _player;  //игрок
    void Start()
    {
        originalX = transform.position.x;
        originalY = transform.position.y;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        transform.position = new Vector2(originalX + (Mathf.Cos(Time.time * timeScaleX) * floatStrengthX), originalY + (Mathf.Sin(Time.time * timeScaleY) * floatStrengthY));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "Pinguin" && other.tag == "Bullet")
        {
            gameObject.transform.position = _player.transform.position;
            originalX = transform.position.x;
            originalY = transform.position.y;
            _playerMovement.Dead();
        }
    }
}
