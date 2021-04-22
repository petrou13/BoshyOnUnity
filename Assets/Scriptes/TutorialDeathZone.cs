using UnityEngine;

public class TutorialDeathZone : MonoBehaviour
{
    private Rigidbody2D body;  //тело игрока
    public Vector3 restartPosition = new Vector3(-8.68f, -2.06f, 1.1f);  //позиция рестарта
    void Start()
    {
        body = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)  //перемещение игрока
    {
        body.transform.position = restartPosition;
    }
}
