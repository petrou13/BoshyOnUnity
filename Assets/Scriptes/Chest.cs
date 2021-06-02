using System.Collections;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator animator; //аниматор текста
    public TextMeshProUGUI textDisplay;  //выводимый текст

    [SerializeField] private float typingSpeed;  //скорость появления каждой буквы в предложении
    [SerializeField] private float floatStrength = 0.01f;  //высота полета
    [SerializeField] private int timeScale = 5;  //ускорение полета
    private int triggered = -1;
    private Canvas dialogCanvas;  //сам canvas диалога
    private float memoriseTypingSpeed;  //запоминание скорости появления букв
    private int index;  //счетчик предложений
    public string[] dialog;  //предложения в диалоге
    private float originalY;  //изначальная позиция по Y

    void Start()
    {
        dialogCanvas = GameObject.FindGameObjectWithTag("DialogCanvas").GetComponent<Canvas>();
        memoriseTypingSpeed = typingSpeed;
        originalY = transform.position.y;
    }

    private void Update()  //парение объекта
    {
        transform.position = new Vector2(transform.position.x, originalY + (Mathf.Sin(Time.time * timeScale) * floatStrength));
        if (triggered == 0)
        {
            triggered = 1;
            dialogCanvas.enabled = true;
            StartCoroutine(Type());
        }
        if (Input.GetKeyDown(KeyCode.E) && triggered == 1)
        {
            triggered = 2;
            dialogCanvas.enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)  //начало вывода сообщения
    {
        if (other.CompareTag("Player") && triggered < 0)
        {
            triggered = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = 2;
            dialogCanvas.enabled = false;
            Destroy(gameObject);
        }
    }

    IEnumerator Type()  //печать предложений, переход к следующему предложению
    {
        typingSpeed = memoriseTypingSpeed;

        if (index < dialog.Length)
        {
            animator.SetTrigger("Change");
            textDisplay.text = "";
            foreach (char letter in dialog[index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            index++;
        }
    }
}
