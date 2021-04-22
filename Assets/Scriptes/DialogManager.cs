using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    public Animator animator; //аниматор текста
    public TextMeshProUGUI textDisplay;  //выводимый текст
    private Canvas dialogCanvas;  //сам canvas диалога
    public string[] dialog;  //предложения в диалоге
    public float typingSpeed;  //скорость появления каждой буквы в предложении
    private float memoriseTypingSpeed;  //запоминание скорости появления букв
    private int index;  //счетчик предложений
    private bool canContinue = true, dialogEnded = false;  //фикс многократного нажатия

    void Start()
    {
        dialogCanvas = GameObject.FindGameObjectWithTag("DialogCanvas").GetComponent<Canvas>();
        memoriseTypingSpeed = typingSpeed;
    }

    IEnumerator Type()  //печать предложений, переход к следующему предложению
    {
        typingSpeed = memoriseTypingSpeed;

        canContinue = false;
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
            canContinue = true;
        }
        else
        {
            dialogEnded = true;
            dialogCanvas.enabled = false;
        }
    }


    public void StartDialog()  //начать диалог, при повторном нажатии увеличивается скорость печати
    {
        //Debug.Log("canContinue = '" + canContinue + "' dialogEnded = '" + dialogEnded + "' index = '" + index + "'" + " dialog.Length = '" + dialog.Length + "'");
        if (canContinue && !dialogEnded)
        {
            StartCoroutine(Type());
        }
        else if (dialogEnded && index == dialog.Length)
        {
            dialogEnded = false;
            textDisplay.text = "";
            index--;
            StartCoroutine(Type());
        }
        else
        {
            typingSpeed = 0.0001f;
        }
    }

    public void StopDialog()  //типа оптимизация, выход игрока рестартает текущее предложение
    {
        textDisplay.text = "";
        canContinue = true;
        StopAllCoroutines();
    }
}
