using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;  //работа с чекпоинтами и перезагрузкой сцены
    private Rigidbody2D body;  //тело игрока
    private SpriteRenderer spriteRenderer;  //спрайт игрока
    public GameObject deathScreen;  //скрин после смерти
    public Transform groundCheck;  //для проверки нахождения игрока на земле
    private bool isGrounded, isJumping = false;
    public bool facingRight = true; //isMoving,
    public float moveSpeed = 1, jumpForce = 1.75f, maxSpeed = 3, jumpTime = 0.125f;  //переменные ходьбы, прыжка, максимальной скорости игрока, прыжка по нажатию
    public int curJumps = 1, maxJumps = 2;  //текущее количество прыжков, выполненное до приземления и максимальное количество прыжков
    private float jumpTimeCounter;  //для работы прыжка по нажатию клавиши
    public bool gravityJumping = false;  //включено ли изменение гравитации игрока по нажатию на кнопку прыжка
    public bool isGravityChanged = false;  //изменена ли гравитация
    public Vector2 movement = new Vector2();  //передвижение перса
    public bool isDead = false;  //для объектов, которые респавняться, чтобы не было ошибок

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = gameManager.savedPosition;  //сохраненная позиция
    }

    void Update()
    {
        Move();

        if (!gravityJumping)
        {
            if (Input.GetKeyDown(KeyCode.Z) && isGrounded)  //прыжок на земле
            {
                Jump();
                curJumps = 0;
            }
            if (Input.GetKey(KeyCode.Z) && isJumping) //увеличение высоты прыжка по нажатию
            {
                JumpByHolding();
            }
            if (Input.GetKeyUp(KeyCode.Z) && isJumping) //игрок отпустил клавишу прыжка
            {
                isJumping = false;
                curJumps++;
            }
            if (Input.GetKeyDown(KeyCode.Z) && curJumps < maxJumps && !isGrounded)  //двойной прыжок
            {
                Jump();
                JumpByHolding();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z) && isGrounded)  //прыжок на земле
            {
                ChangeGravity();
            }
        }
    }

    private void FixedUpdate()
    {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);  //максимальная скорость

        isGrounded = transform.Find("groundCheck").GetComponent<GroundCheck>().isGrounded;  //проверка есть ли под игроком земля

        if (isGrounded && curJumps == 2)   //сброс счетчика текущих прыжков при приземлении
        {
            curJumps = 0;
        }
    }

    void Jump()  //прыжок
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;
        if (isGravityChanged)
        {
            movement.y = -jumpForce;
            body.velocity = new Vector2(body.velocity.x, movement.y);
        }
        else
        {
            movement.y = jumpForce;
            body.velocity = new Vector2(body.velocity.x, movement.y);
        }
    }

    void JumpByHolding()  //удержание клавиши приводит к увеличению высоты прыжка
    {
        if (jumpTimeCounter > 0)
        {
            if (isGravityChanged)
            {
                movement.y = -jumpForce;
                body.velocity = new Vector2(body.velocity.x, movement.y);
            }
            else
            {
                movement.y = jumpForce;
                body.velocity = new Vector2(body.velocity.x, movement.y);
            }
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
            curJumps++;
        }
    }

    void ChangeGravity()  //смена гравитации
    {
        body.gravityScale *= -1;
        transform.Rotate(180f, 0f, 0f);
    }

    void Move()  //движение игрока
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))  //движение направо
        {
            if (!facingRight)  //если смотрит направо, то поворачиваем налево
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))  //движение налево
        {
            if (facingRight) //если не смотрит направо, то поворачиваем направо
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
        }
        //transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed));
        movement = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, movement.y);
        body.velocity = new Vector2(movement.x, body.velocity.y);
    }


    public void Dead()  //смерть гг
    {
        isDead = true;
        Destroy(gameObject);
        deathScreen.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other)  //попадание на врага
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Dead();
        }
    }

}