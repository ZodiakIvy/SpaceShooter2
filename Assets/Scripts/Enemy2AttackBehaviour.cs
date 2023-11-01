using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2AttackBehaviour : MonoBehaviour
{
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemy2Attack();
    }

    void Enemy2Attack()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();

            if (player != null)
            {
                player.Damage();
            }
        }
    }
}
