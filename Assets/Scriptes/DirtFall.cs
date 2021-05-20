using UnityEngine;

public class DirtFall : MonoBehaviour
{
    [SerializeField] private float fallingSpeed = 0.01f;  //скорость падения
    private bool isFalling = false;  //падает ли блок
    private float originalY;  //изначальное положение по Y

    private void OnTriggerEnter2D(Collider2D other)  //игрок заходит - начинает падать
    {
        originalY = transform.position.y;
        if (other.tag == "Player")
        {
            isFalling = true;
        }
    }
    private void Update()
    {
        if (isFalling)  //если падает
        {
            if (originalY - transform.position.y == 50f)  //если упал на 50 пунктов
            {
                isFalling = false;
                gameObject.SetActive(false);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - fallingSpeed);  //процесс падания
            }
        }
    }
}
