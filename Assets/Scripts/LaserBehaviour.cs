﻿using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;
    [SerializeField]
<<<<<<< HEAD
    private GameObject _laser;
    [SerializeField]
    private bool _enemy1Attack = false;
   
=======
    private bool _enemy1Attack = false;
>>>>>>> bc922f14701ad25fc66295d5799fea60dc902124

    // Update is called once per frame
    void Update()
    {
        
        if (_enemy1Attack == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
      
    }

    void MoveUp()
    {
        //translate laser up
        transform.position += (new Vector3(0, 1, 0) * _laserSpeed * Time.deltaTime);
        if (transform.position.y >= 5.3f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        //translate laser down
        transform.position += (new Vector3(0, -1, 0) * _laserSpeed * Time.deltaTime);
        if (transform.position.y <= -5.3f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _enemy1Attack = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _enemy1Attack == true)
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            
            if (player != null)
            {
                player.Damage();
            }
        }
    }
}
