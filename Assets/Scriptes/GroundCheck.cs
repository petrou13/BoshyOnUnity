using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    public LayerMask groundLayer;  //выбор слоя земли

    private void OnTriggerStay2D(Collider2D other)
    {
        isGrounded = other != null && (((1 << other.gameObject.layer) & groundLayer) != 0);  //если колайдер не пустой и слои совпадают
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
}
