using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public GameObject deathEffect;

    public void TakeDamage(int damage) //получение урона
    {
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
