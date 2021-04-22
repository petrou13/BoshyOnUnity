using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    private static GameManager instance;
    [SerializeField]
    public Vector2 savedPosition;
    void Awake()
    {
        if (instance == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            savedPosition = player.transform.position;
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
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
