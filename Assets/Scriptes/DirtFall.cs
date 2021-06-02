using UnityEngine;

public class DirtFall : MonoBehaviour
{
    [SerializeField] private float fallingSpeed = 0.005f;  //скорость падения
    [SerializeField] private string onTag = "Player";  //падение на какой тег
    [SerializeField] private bool offCollider = false;  //выключать коллайдер при падении
    [SerializeField] Collider2D colliderOff;  //какой коллайдер выключаем
    private bool isFalling = false;  //падает ли блок
    private float originalY;  //изначальное положение по Y

    private void OnTriggerEnter2D(Collider2D other)  //игрок заходит - начинает падать
    {
        originalY = transform.position.y;
        if (other.tag == onTag)  //объект падает
        {
            isFalling = true;
            if (offCollider)  //выключение коллайдера
            {
                if (colliderOff == null)
                {
                    colliderOff.GetComponent<Collider2D>().enabled = false;
                }
                else
                {
                    colliderOff.enabled = false;
                }
            }
        }
    }
    private void FixedUpdate()
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
