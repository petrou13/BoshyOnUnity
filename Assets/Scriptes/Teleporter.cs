using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public string sceneName; //название сцены, в которую телепортируемся
    public Animator animator; //аниматор камеры
    void OnTriggerEnter2D(Collider2D other)  //старт корутины телепорта
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Teleport());
        }
    }

    void OnTriggerExit2D(Collider2D other)  //при выходе отмена телепорта и анимации
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            animator.SetTrigger("Exit");
        }
    }

    IEnumerator Teleport()  //телепорт в другую локацию
    {
        animator.SetTrigger("Teleport");
        yield return new WaitForSeconds(0.95f);
        SceneManager.LoadScene(sceneName);
    }
}
