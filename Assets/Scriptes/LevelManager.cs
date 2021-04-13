using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))   //перезагрузка текущей сцены
        {
            SceneManager.LoadScene(scene.name);
        }
    }
}
