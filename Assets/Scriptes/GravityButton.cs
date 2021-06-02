using System.Collections;
using UnityEngine;

public class GravityButton : MonoBehaviour
{
    public BoxCollider2D boxCollider;  //колайдер кнопки

    private Rigidbody2D playerBody;  //тело игрока - смена гравитации
    private GameObject player; //поворот модели игрока
    private PlayerMovement playerMovement;  //переприсваивание isGravityChanged

    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    IEnumerator ChangeGravity()  //смена гравитации и поворот объекта игрока
    {
        playerBody.gravityScale *= -1;
        player.transform.Rotate(180f, 0f, 0f);
        
        boxCollider.enabled = false;  //исправление баги с многократным вхождением в триггер
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) //при заходе игрока - смена гравитации
    {
        if (other.tag == "Player")
        {
            playerMovement.isGravityChanged = !playerMovement.isGravityChanged;
            StartCoroutine(ChangeGravity());
        }
    }

}
