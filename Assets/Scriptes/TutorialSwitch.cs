using TMPro;
using UnityEngine;

public class TutorialSwitch : MonoBehaviour
{
    public GameObject activeFrame;
    public TextMeshProUGUI textMain, textDescription;
    public Canvas tutorialCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            activeFrame.SetActive(true);
        }
        if(other.CompareTag("Player") && activeFrame.name == "FrameFirst")
        {
            textMain.text = "Обучение";
            textDescription.text = "Добро пожаловать в обучение!\nВ этих комнатах вы научитесь вещам, которые вы, скорее всего, знаете!";
        }
        else if(other.CompareTag("Player") && activeFrame.name == "FrameSecond")
        {
            textMain.text = "Ходьба";
            textDescription.text = "Используйте стрелки, чтобы передвигать своего персонажа";
        }
        else if(other.CompareTag("Player") && activeFrame.name == "FrameThird")
        {
            textMain.text = "Прыжок";
            textDescription.text = "Прыжок выполняется на кнопку Z\nЗажмите кнопку Z, чтобы прыгнуть выше\nНажмите кнопку Z в воздухе для 'двойного прыжка'";
        }
        else if(other.CompareTag("Player") && activeFrame.name == "FrameFour")
        {
            textMain.text = "Больше прыжков";
            textDescription.text = "Быстро нажмите кнопку Z для маленького прыжка\nВо время подбора бустера двойного прыжка,\nпоявляется возможность сделать еще один двойной прыжок\nНажмите кнопку Z для еще одного 'двойного прыжка'";
        }
        else if(other.CompareTag("Player") && activeFrame.name == "FrameFive")
        {
            textMain.text = "Стрельба";
            textDescription.text = "Используй кнопку X для выстрела\nВсего можно иметь 5 пуль на экране одновременно\nВыстрел в сейвпоинт сохранит прогресс\nСейвпоинты могут помочь преодолеть большие расстояния\nЭто делается сейвджампингом\nСэйвджампинг выполняется сохранением на высокой точке,\nзатем рестартом (R) и двойным прыжком после рестарта.";
        }
        else
        {
            tutorialCanvas.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            activeFrame.SetActive(false);
        }
    }
}
