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
    private Animator _anim;
    private PlayerBehaviour _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        if (_player == null) 
        { 
            Debug.LogError("Player is NULL"); 
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL");
        }
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
        if (other.CompareTag ("Laser"))
        {
            Destroy(other.gameObject);
            
            if (_player != null)
            {
               _player.Score();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _moveSpeed = 0;

            Destroy(this.gameObject, 2.4f);
            
        }

        if (other.CompareTag ("Player"))
        {
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            player.Damage();
            _anim.SetTrigger("OnEnemyDeath");
            _moveSpeed = 0;
            Destroy(this.gameObject, 2.4f);
        }
                
    }
 
}
