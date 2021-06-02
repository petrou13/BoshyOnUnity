using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;  //эффект смерти противника
    public int health = 1;  //здоровье противника
    public GroundCheck groundCheck;  //проверка, есть ли под объектом земля

    [SerializeField] private Camera cam;  //камера, которая видит игрока
    [SerializeField] private float moveSpeed = 1f;  //скорость
    [SerializeField] private float jumpForce = 6f;  //сила прыжка
    [SerializeField] private bool isInvisible = false;  //бессмертен ли
    [SerializeField] private bool canMove = false;  //может двигаться
    [SerializeField] private bool canJump = false;  //может прыгать
    private GameObject player;  //игрок
    private Rigidbody2D rb;  //rb врага
    private Plane[] planes;  //границы камеры
    private SpriteRenderer spriteRenderer; //спрайт рендерер противника
    private Animator animator;  //аниматор противника
    private bool isDown = true;  //игрок на уровне врага

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (player != null && GeometryUtility.TestPlanesAABB(planes, player.GetComponent<Collider2D>().bounds))
        {
            
            Vector2 direction = player.transform.position - transform.position;
            if (direction.y <= 1 && groundCheck.isGrounded)
            {
                isDown = true;
            }
            if (direction.y > 1 && groundCheck.isGrounded)
            {
                isDown = false;
            }

            if (canMove && isDown)  //если игрок находится на высоте противника, то двигается по оси X
            {
                if (direction.x > 0)
                {
                    rb.AddForce(new Vector2(moveSpeed, 0));  //движения сделаны через force
                    spriteRenderer.flipX = true;
                }
                if (direction.x < 0)
                {
                    rb.AddForce(new Vector2(-moveSpeed, 0));
                    spriteRenderer.flipX = false;
                }
            }
            if (canJump && !isDown)  //если выше него, то двигается по оси X и Y
            {
                if (groundCheck.isGrounded)
                {
                    rb.velocity = new Vector2(0, jumpForce);
                }

                if (direction.x > 0)
                {
                    rb.velocity = new Vector2(moveSpeed / 10, rb.velocity.y);  //движения сделаны через velocity
                    spriteRenderer.flipX = true;
                }
                if (direction.x < 0)
                {
                    rb.velocity = new Vector2(-moveSpeed / 10, rb.velocity.y);
                    spriteRenderer.flipX = false;
                }
            }
        }
        animator.SetFloat("Speed", rb.velocity.x);  //скорость проигрывания анимации + работа анимации
    }

    public void TakeDamage(int damage) //получение урона
    {
        if (!isInvisible)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage));
            }

            health -= damage;
            if (health <= 0)
            {
                Dead();
            }
        }
    }

    void Dead()  //смерть противника
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
        }
        Destroy(gameObject);
    }
}
