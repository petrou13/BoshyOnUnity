using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFall : MonoBehaviour
{
    [SerializeField] private GameObject spike;  //шипы
    [SerializeField] private float fallingSpeed = 0.05f; //скорость падения
    private bool isFalling = false;  //падает ли блок
    private float originalY;  //изначальное положение по Y
    private void OnTriggerEnter2D(Collider2D other)  //игрок заходит - начинает падать
    {
        originalY = transform.position.y;
        if (other.tag == "Player")
        {
            isFalling = true;
            spike.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (isFalling)  //если падает
        {
            if (originalY - transform.position.y == 50f)  //если упал на 50 пунктов
            {
                isFalling = false;
                spike.SetActive(false);
            }
            else
            {
                spike.transform.position = new Vector2(spike.transform.position.x, spike.transform.position.y - fallingSpeed);  //процесс падания
            }
        }
    }
}
