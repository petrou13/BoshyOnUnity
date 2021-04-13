using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D body;
    SpriteRenderer spriteRenderer;
    public GameObject deathScreen;
    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGrounded, facingRight = true;
    public float moveSpeed = 1, jumpForce = 3, maxSpeed = 3;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = gameManager.savedPosition;  //сохраненная позиция
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))  //движение направо
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow))  //движение налево
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)  //прыжок
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);  //максимальная скорость

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);  //стоит ли игрок на земле
    }

    void Jump()  //прыжок   ///////////////////////////////////////////////ДОДЕЛАТЬ СИЛУ ПРЫЖКА ПО НАЖАТИЮ НА СТРЕЛКУ
    {
        if (isGrounded)
        {
            body.velocity = Vector2.up * jumpForce;
        }
    }

    void MoveLeft()  //движение игрока налево
    {
        if (facingRight)  //если смотрит направо, то поворачиваем налево
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        }
        transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed));
    }

    void MoveRight()  //движение игрока направо
    {
        if (!facingRight) //если не смотрит направо, то поворачиваем направо
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;
        }
        transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed));
    }

    public void Dead()  //смерть гг
    {
        Destroy(gameObject);
        deathScreen.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other)  //попадание на врага
    {
        if(other.gameObject.tag.Equals("Enemy"))
        {
            Dead();
        }
    }

}
