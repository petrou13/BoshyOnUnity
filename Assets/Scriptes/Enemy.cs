using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public GameObject deathEffect;

    public void TakeDamage(int damage) //получение урона
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage));
        }

        health -= damage;
        if (health <= 0)
        {
            Dead();
        }
    }

    void Dead()  //смерть противника
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect, 5f);
    }

}
