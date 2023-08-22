using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Enemy1Behaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy1;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _moveSpeed = 4;
    
    private PlayerBehaviour _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy1Movement();    
    }

    void Enemy1Movement()
    {
        transform.position += (new Vector3(0, -1, 0) * _moveSpeed * Time.deltaTime);
        if (transform.position.y <= -5.3f)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            
            if (_player != null)
            {
                _player.Score();
            }

            Destroy(this.gameObject);
            
        }

        if (other.tag == "Player")
        {
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            other.transform.GetComponent<PlayerBehaviour>().Damage();
            Destroy(this.gameObject);
        }
                
    }
 
}
