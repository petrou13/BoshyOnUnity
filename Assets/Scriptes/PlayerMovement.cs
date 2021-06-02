using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public GameObject deathScreen;  //скрин после смерти
    public Transform groundCheck;  //для проверки нахождения игрока на земле
    public Vector2 movement = new Vector2();  //передвижение перса
    public bool facingRight = true; //isMoving,
    public float moveSpeed = 1, jumpForce = 1.9f, maxSpeed = 3, jumpTime = 0.125f;  //переменные ходьбы, прыжка, максимальной скорости игрока, прыжка по нажатию
    public int curJumps = 0;  //текущее количество прыжков, выполненное до приземления
    public bool gravityJumping = false;  //включено ли изменение гравитации игрока по нажатию на кнопку прыжка
    public bool isGravityChanged = false;  //изменена ли гравитация
    public bool isDead = false;  //для объектов, которые респавняться, чтобы не было ошибок

    [SerializeField] private int maxJumps = 2; //максимальное количество прыжков
    [SerializeField] private GameObject bloodParticle;
    private GameManager gameManager;  //работа с чекпоинтами и перезагрузкой сцены
    private Rigidbody2D body;  //тело игрока
    private SpriteRenderer spriteRenderer;  //спрайт игрока
    private bool isGrounded, isJumping = false;
    private float jumpTimeCounter;  //для работы прыжка по нажатию клавиши
    private AudioManager _audioManager;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = gameManager.savedPosition;  //сохраненная позиция
        _audioManager = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioManager>();
    }

    void Update()
    {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);  //максимальная скорость
        isGrounded = transform.Find("groundCheck").GetComponent<GroundCheck>().isGrounded;  //проверка есть ли под игроком земля

        if (isGrounded && !isJumping)   //сброс счетчика текущих прыжков при приземлении
        {
            curJumps = 0;
        }

        Move();
        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal") * moveSpeed));

        if (!gravityJumping)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && isGrounded)  //прыжок на земле
        {
            ChangeGravity();
        }
    }

    void Jump()  //прыжок
    {
        if (Input.GetKeyDown(KeyCode.Z) && curJumps < maxJumps)  //прыжок на земле
        {
            curJumps++;  //увеличение количества текущих прыжков
            jumpTimeCounter = jumpTime;  //сброс таймера прыжка

            if (!isGrounded && curJumps < maxJumps)  //если первый прыжок начинается в полете
            {
                curJumps++;
            }

            if (curJumps == 1)  //звук первого прыжка
            {
                SFXManager.PlaySound("jump1");
            }
            else if (curJumps >= 2)
            {
                SFXManager.PlaySound("jump2");  //звук двойного прыжка
            }
            isJumping = true;  //игрок прыгает
        }

        if (Input.GetKey(KeyCode.Z) && isJumping) //увеличение высоты прыжка по нажатию
        {
            jumpTimeCounter -= Time.deltaTime;
            if (jumpTimeCounter > 0)
            {
                if (isGravityChanged)
                {
                    movement.y = -jumpForce;
                }
                else
                {
                    movement.y = jumpForce;
                }
                body.velocity = new Vector2(body.velocity.x, movement.y);
            }
        }

        if (Input.GetKeyUp(KeyCode.Z) && isJumping) //игрок отпустил клавишу прыжка
        {
            isJumping = false;
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
        Instantiate(bloodParticle, transform.position, Quaternion.identity);  //частицы крови при смерти
        Destroy(gameObject);
        _audioManager.PlayerDead();
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