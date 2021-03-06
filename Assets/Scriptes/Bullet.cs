using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impactEffect;  //эффект во время столкновения с чем-то
    public Rigidbody2D body;  //тело пули
    public float bulletSpeed;  //скорость пули
    public int damage = 1;  //наносимый урон
    
    private Weapon weapon;  //скрипт оружия игрока

    void Start()
    {
        body.velocity = transform.right * bulletSpeed;  //передвижение пули
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)   //уничтожение пули, нанесение урона
    {
        if (hitInfo.name != "Player" && hitInfo.tag != "Confiner" && hitInfo.tag != "NPC" && hitInfo.tag != "JumpBuster" && hitInfo.tag != "Edge")
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(effect, 1f);
        }
    }
}
