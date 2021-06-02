using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    GameManager gameManager;  //сохранение
    GameObject player;  //игрок
    [SerializeField] private GameObject floatingText;  //вылетающий текст
    [SerializeField] private Vector3 offset = new Vector3(0f, 0.05f, 0);  //изменение начальной позиции текста
    [SerializeField] private GameObject bullet;  //префаб пули
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)  //при попадании в чекпоинт, происходит сохранение текущей позиции и сцены игрока
    {
        if (other.tag == "Bullet")
        {
            gameManager.savedPosition = player.transform.position;  //сохранение
            gameManager.scene = SceneManager.GetActiveScene().buildIndex;
            SaveSystem.SavePlayer(gameManager);

            ShowFloatingText();  //показ текста

            if (bullet != null) 
            {
                Vector3 targ = player.transform.position;
                targ.z = 0f;
                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            }
        }
    }

    private void ShowFloatingText()  //появление и уничтожение вылетающего текста
    {
        GameObject text = Instantiate(floatingText, player.transform.position, Quaternion.identity, transform);  //появление на игроке
        text.transform.localPosition += offset;
        Destroy(text, 1f);
    }
}
