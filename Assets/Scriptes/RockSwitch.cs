using UnityEngine;

public class RockSwitch : MonoBehaviour
{
    [SerializeField] private float timeSwitch = 0.75f;  //время для пропадания объекта
    [SerializeField] private bool isVisible = true;  //объект осязаем
    [SerializeField] private Sprite spriteVisible;  //спрайт осязаемого объекта
    [SerializeField] private Sprite spriteInvisible;  //спрайт, где объект будет находиться
    private Collider2D colliderObj;  //коллайдер объекта
    private SpriteRenderer spriteRenderer;  //рендерер спрайта объекта
    private float startTime;  //сохранение значения timeSwitch
    void Start()  //присваивание значений переменным, делаем объект видимым/невидимым
    {
        startTime = timeSwitch;
        colliderObj = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        colliderObj.enabled = isVisible;
        SpriteChange();
    }

    void FixedUpdate()  //по истечению определенного времени меняется спрайт, вкл/выкл коллайдер
    {
        timeSwitch -= Time.deltaTime;
        if (timeSwitch <= 0)
        {
            isVisible = !isVisible;
            colliderObj.enabled = isVisible;
            SpriteChange();
            timeSwitch = startTime;
        }
    }

    private void SpriteChange()
    {
        if (isVisible)
        {
            spriteRenderer.sprite = spriteVisible;
        }
        else
        {
            spriteRenderer.sprite = spriteInvisible;
        }
    }
}
