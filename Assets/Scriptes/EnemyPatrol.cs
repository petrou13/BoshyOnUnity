using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;  //скорость передвижения
    private Rigidbody2D rb;  //тело объекта
    private SpriteRenderer spriteRenderer;  //спрайт рендерер объекта
    private Animator animator;  //аниматор объекта
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        moveSpeed = -moveSpeed;  //изменение скорости на противоположную
    }

    void FixedUpdate()  //передвижение объекта, установка параметра анимации
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.x);
    }

    private void OnTriggerEnter2D(Collider2D other)  //если встретил стену
    {
        if (other.tag != "Player" || other.tag != "Bullet")
        {
            moveSpeed = -moveSpeed;  //изменение скорости на противоположную
            spriteRenderer.flipX = !spriteRenderer.flipX;  //флип спрайта
        }
    }
}
