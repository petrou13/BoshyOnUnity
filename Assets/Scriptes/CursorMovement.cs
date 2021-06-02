using UnityEngine;
using System.Runtime.InteropServices;

public class CursorMovement : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);  //передвинуть курсор куда-то
    [SerializeField] private float cursorSpeed = 1f;  //скорость передвижения курсора
    [SerializeField] private float timeBeforeMoving = 5;  //время до движения курсора
    [SerializeField] private bool isCursorDeadly = true; //убивает ли курсор
    [SerializeField] private bool isCursorTeleport = false;
    private GameObject player;  //игрок
    private PlayerMovement _playerMovement;  //скрипт игрока
    private Vector2 cursorPos;  //где находится курсор
    private Vector2 moveTo;  //куда курсор двигается
    private bool isMoving;  //игрок двигает курсор
    private bool onScreen; //курсор на экране

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        moveTo = Input.mousePosition;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            isCursorDeadly = !isCursorDeadly;
            isCursorTeleport = !isCursorTeleport;
        }
        if (isCursorTeleport && player != null)
        {
            cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                player.transform.position = cursorPos;
            }
        }
    }

    void FixedUpdate()
    {
        if (isCursorDeadly && Camera.main != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);  //считывание наведение курсора на игрока

            MouseMoved();
            MouseNotAtScreen();

            if (timeBeforeMoving >= 0)  //таймер для запуска передвижения курсора
            {
                timeBeforeMoving -= Time.deltaTime;
            }
            else if (_playerMovement != null && Time.timeScale != 0 && !isMoving && onScreen && timeBeforeMoving <= 0)
            {
                CursorMove();
            }

            if (hit.collider != null && hit.collider.tag == ("Player") && onScreen)  //смерть игрока при достижении его курсором
            {
                _playerMovement.Dead();
            }
        }
    }

    private void MouseMoved()  //курсор двигает игрок
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            isMoving = true;
            timeBeforeMoving = 5;
        }
        else
        {
            isMoving = false;
            moveTo = Input.mousePosition;
        }
    }

    private void MouseNotAtScreen()
    {
        if (Input.mousePosition.x == 0 || Input.mousePosition.y == 0 || Input.mousePosition.x == Screen.width - 1 || Input.mousePosition.y == Screen.height - 1)
        {
            onScreen = false;
        }
        else
        {
            onScreen = true;
        }
    }

    private void CursorMove()  //передвижение курсора
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cursorMove = new Vector2();
        if (!_playerMovement.isDead)
        {
            cursorMove = Vector2.Lerp(cursorPos, player.transform.position, 1);
        }

        if ((int)Input.mousePosition.x < (int)Camera.main.WorldToScreenPoint(cursorMove).x)  //плавное передвижение курсора
        {
            moveTo.x += cursorSpeed;
        }
        if ((int)Input.mousePosition.x > (int)Camera.main.WorldToScreenPoint(cursorMove).x)
        {
            moveTo.x -= cursorSpeed;
        }

        if ((int)Input.mousePosition.y < (int)Camera.main.WorldToScreenPoint(cursorMove).y)  //up
        {
            moveTo.y += cursorSpeed * 2;
        }
        if ((int)Input.mousePosition.y > (int)Camera.main.WorldToScreenPoint(cursorMove).y)  //down
        {
            moveTo.y -= cursorSpeed;
        }

        SetCursorPos((int)moveTo.x, Screen.height - (int)moveTo.y);  //переместить курсор
        //SetCursorPos(Convert.ToInt32(Camera.main.WorldToScreenPoint(cursorMove).x), Screen.height - Convert.ToInt32(Camera.main.WorldToScreenPoint(cursorMove).y));
    }

    public PlayerMovement PlayerMovement
    {
        get => default;
        set
        {
        }
    }
}
