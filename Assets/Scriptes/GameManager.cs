using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject player;  //игрок
    [SerializeField]
    public Vector2 savedPosition;  //сохраненная позиция
    public int scene;  //сохраненная сцена
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (SaveSystem.LoadPlayer() != null)  //если есть файл с сохранениями
        {
            PlayerData data = SaveSystem.LoadPlayer();  //загружаем инфу с файла
            if (data.scene == SceneManager.GetActiveScene().buildIndex)
            {
                savedPosition.x = data.position[0];  //позиция по x
                savedPosition.y = data.position[1];  //позиция по y
                scene = data.scene;  //номер уровня
            }
            else
            {
                savedPosition = player.transform.position;
                scene = SceneManager.GetActiveScene().buildIndex;
            }
        }
        else
        {
            savedPosition = player.transform.position;
            scene = SceneManager.GetActiveScene().buildIndex;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))   //перезагрузка текущей сцены с чекпоинтами
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
