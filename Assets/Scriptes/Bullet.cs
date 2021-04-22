using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Weapon weapon;
    public float bulletSpeed;
    public int damage = 1;
    public GameObject impactEffect;
    public Rigidbody2D body;
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
            Destroy(effect, 5f);
        }
    }
}
