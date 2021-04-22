using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool autoShoot = false;
    public float fireRate = 0.12f;
    private float nextFire = 0.0f;
    private int bulletsFired = 0;
    public Transform firePoint;
    public GameObject bulletPrefub;

    void Update()
    {
        if (!autoShoot)
        {
            if (Input.GetKeyDown(KeyCode.X))  //стрельба без автострельбы
            {
                Shoot();       //Может поставить ограничения?
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.X) && Time.time > nextFire && bulletsFired != 5)  //стрельба с автострельбой
            {
                nextFire = Time.time + fireRate;  //задержка в стрельбе
                Shoot();
                bulletsFired++;
            }
            else if (Input.GetKey(KeyCode.X) && bulletsFired == 5)  //исключение возможности бесконечной стрельбы
            {
                bulletsFired = 0;
                nextFire = Time.time + 1;
            }

            if (Input.GetKeyUp(KeyCode.X))  //если игрок соблюдает тайминги, то это ему поощрается и счетчик сбрасывается
            {
                bulletsFired = 0;
            }
        }

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefub, firePoint.position, firePoint.rotation);  //выстрел пули
        Destroy(bullet, 5f);
    }
}
