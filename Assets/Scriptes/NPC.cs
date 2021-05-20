using UnityEngine;

public class NPC : MonoBehaviour
{
    private GameObject hint;
    private DialogManager dialogManager;
    private Canvas dialogCanvas;
    private bool triggered = false;
    
    void Start()
    {
        dialogCanvas = GameObject.FindGameObjectWithTag("DialogCanvas").GetComponent<Canvas>();
        dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
        hint = GameObject.FindGameObjectWithTag("TalkInfo");
        hint.SetActive(false);
    }

    void Update()
    {
        if(triggered && Input.GetKeyDown(KeyCode.E))  //начало разговора, смена предложений
        {
            dialogCanvas.enabled = true;
            dialogManager.StartDialog();
        }
    }

    void OnTriggerEnter2D(Collider2D other)  //всплывание подсказки, возможность заговорить с нпц
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
            hint.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)  //убратие подсказки, нет возможности разговора
    {
        if (other.CompareTag("Player"))
        {
            triggered = false;
            hint.SetActive(false);
            dialogCanvas.enabled = false;
            dialogManager.StopDialog();
        }
    }
}
