using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D body;
    void Start()
    {
        body.velocity = transform.right * bulletSpeed;
    }
    
    void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        if(hitInfo.name != "Player")
        {
            Destroy(gameObject);
        }
    }
}
