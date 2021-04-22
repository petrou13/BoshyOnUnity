using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private float xInput;
    private GameManager gameManager;  //работа с чекпоинтами и перезагрузкой сцены
    private Rigidbody2D body;  //тело игрока
    private SpriteRenderer spriteRenderer;  //спрайт игрока
    public GameObject deathScreen;  //скрин после смерти
    public Transform groundCheck;  //для проверки нахождения игрока на земле
    public LayerMask groundLayer;  //выбор слоя земли
    private bool isGrounded, isJumping = false, facingRight = true; //isMoving,
    public float moveSpeed = 1, jumpForce = 1.75f, maxSpeed = 3, jumpTime = 0.125f;
    public int curJumps = 0, maxJumps = 2;
    private float jumpTimeCounter;
    public bool gravityJumping = false;
    private bool isGravityChanged = false;

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
            if (!facingRight)  //если смотрит направо, то поворачиваем налево
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
            Movement();
        }
        if (Input.GetKey(KeyCode.LeftArrow))  //движение налево
        {
            if (facingRight) //если не смотрит направо, то поворачиваем направо
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
            Movement();
        }

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

            if (isGrounded && curJumps == 2)   //сброс счетчика текущих прыжков при приземлении
            {
                curJumps = 0;
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

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundLayer);  //круг на проверку земли под игроком
    }

    void Jump()  //прыжок
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;
        if (isGravityChanged)
        {
            body.velocity = Vector2.down * jumpForce;
        }
        else
        {
            body.velocity = Vector2.up * jumpForce;
        }
    }

    void JumpByHolding()  //удержание клавиши приводит к увеличению высоты прыжка
    {
        if (jumpTimeCounter > 0)
        {
            if (isGravityChanged)
            {
                body.velocity = Vector2.down * jumpForce;
            }
            else
            {
                body.velocity = Vector2.up * jumpForce;
            }
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
            curJumps++;
        }
    }

    void ChangeGravity()
    {
        body.gravityScale *= -1;
        transform.Rotate(180f, 0f, 0f);
    }

    void Movement()  //движение игрока
    {
        // xInput = Input.GetAxisRaw("Horizontal");

        // isMoving = (xInput != 0);
        // if (isMoving)
        // {
        //     Vector3 moveDir = new Vector3(xInput, transform.position.y, transform.position.z);
        //     body.MovePosition(new Vector2((transform.position.x + moveDir.x * moveSpeed * Time.deltaTime), (transform.position.y + moveDir.y * moveSpeed * Time.deltaTime)));

        // }
        transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed));
    }


    public void Dead()  //смерть гг
    {
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "GravityButton")
        {
            isGravityChanged = !isGravityChanged;
        }
    }

}