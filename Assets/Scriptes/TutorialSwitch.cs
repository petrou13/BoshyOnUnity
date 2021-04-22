using TMPro;
using UnityEngine;

public class TutorialSwitch : MonoBehaviour
{
    public TutorialDeathZone zone;
    public GameObject activeFrame;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            activeFrame.SetActive(true);
        }
        if (other.CompareTag("Player") && activeFrame.name == "FrameFirst")
        {
            zone.restartPosition = new Vector3(-8.68f, -2.06f, 0f);
        }
        if (other.CompareTag("Player") && activeFrame.name == "FrameSecond")
        {
            zone.restartPosition = new Vector3(1.9f, -3.23f, 0f);
        }
        if (other.CompareTag("Player") && activeFrame.name == "FrameThird")
        {
            zone.restartPosition = new Vector3(4.14f, -3.224f, 0f);
        }
        if (other.CompareTag("Player") && activeFrame.name == "FrameFour")
        {
            zone.restartPosition = new Vector3(10f, -2.24f, 0f);
        }
        if (other.CompareTag("Player") && activeFrame.name == "FrameFive")
        {
            zone.restartPosition = new Vector3(14.481f, -2.46f, 0f);
        }
        if(other.CompareTag("Player") && activeFrame.name == "FrameSix")
        {
            zone.restartPosition = new Vector3(18.788f, -2.464f, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            activeFrame.SetActive(false);
        }
    }
}
