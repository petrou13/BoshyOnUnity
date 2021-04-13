using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer spriteRenderer;
    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGrounded, facingRight = true;
    public float moveSpeed, jumpForce, maxSpeed;
    float xInput, yInput;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
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
        if(isGrounded)
        {
            body.velocity = Vector2.up * jumpForce;
        }
    }

    void MoveLeft()  //движение игрока налево
    {
        if(facingRight)  //если смотрит направо, то поворачиваем налево
        {
            transform.Rotate(0f, 180f, 0f); 
            facingRight = false;
        }
        transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed));
    }

    void MoveRight()  //движение игрока направо
    {
        if(!facingRight) //если не смотрит направо, то поворачиваем направо
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;
        }
        transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed));
    }

}
