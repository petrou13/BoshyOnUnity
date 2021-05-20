using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool autoShoot = false;  //автострельба
    public float fireRate = 0.12f;  //скорострельность
    public float destroyTime = 1f;  //через сколько уничтожить пулю
    public int maxBullets = 5;
    public Transform firePoint;  //точка, откуда летят пули
    public GameObject bulletPrefub;  //сама пуля

    private float nextFire = 0.0f;  //задержка автострельбы
    private int bulletsFired = 0;  //всего выстрелено

    void Update()
    {
        bulletsFired = GameObject.FindGameObjectsWithTag("Bullet").Length;
        if (!autoShoot && bulletsFired < maxBullets)
        {
            if (Input.GetKeyDown(KeyCode.X))  //стрельба без автострельбы
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.X) && Time.time > nextFire && bulletsFired < maxBullets)  //стрельба с автострельбой
            {
                nextFire = Time.time + fireRate;  //задержка в стрельбе
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefub, firePoint.position, firePoint.rotation);  //выстрел пули
        SFXManager.PlaySound("shoot");  //звук стрельбы
        Destroy(bullet, destroyTime);
    }
}
