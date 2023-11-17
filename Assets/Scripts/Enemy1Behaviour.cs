﻿using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy1Behaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy1AttackPrefab;
    private float _fireRate = 3f;
    private float _canFire = -1f;
    [SerializeField]
    private float _moveSpeed = 4;
    [SerializeField]
    private Animator _anim;
    private PlayerBehaviour _player;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explosion_sound;
    [SerializeField]
    private Rigidbody2D _enemyRigidbody;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform[] _powerUpTransform;
    [SerializeField]
    private float _rammingDistance = 2f;
    [SerializeField]
    private float _rammingForce = 10f;
    [SerializeField]
    private Vector3 _originalPosition;
  

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

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the Enemy1 is NULL");
        }
        else
        {
            _audioSource.clip = _explosion_sound;
        }


    }

    // Update is called once per frame
    void Update()
    {
        Enemy1Movement();

        float distance = Vector3.Distance(transform.position, _playerTransform.position);
        int[] x = { 0, 1, 2, 3, 4, 5, 6, 7 };
        float powerUpdistance = Vector3.Distance(transform.position, _powerUpTransform[x[0]].transform.position);
        if (distance < _rammingDistance)
        {
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            _enemyRigidbody.AddForce(direction * _rammingForce);
        }

        if (powerUpdistance < _rammingDistance && Time.time > _canFire)
        {
            _fireRate = .5f;
            _canFire = Time.time + _fireRate;

            GameObject newEnemy1Attack = Instantiate(_enemy1AttackPrefab, transform.position + new Vector3(0, -2.75f, 0), Quaternion.identity);

            LaserBehaviour[] lasers = newEnemy1Attack.GetComponentsInChildren<LaserBehaviour>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
     

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;

            GameObject newEnemy1Attack = Instantiate(_enemy1AttackPrefab, transform.position + new Vector3(0, -2.75f, 0), Quaternion.identity);

            LaserBehaviour[] lasers = newEnemy1Attack.GetComponentsInChildren<LaserBehaviour>();


            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }

    }

    public enum MovementState
    {
        Up,
        Right,
        Down,
        Left
    }

    public MovementState moveState = MovementState.Down;
    //New Enemy Movement
    //Task:
    //Enable the Enemies to move in a new way, either from side to side, circling, or coming into the play field at an angle.
    void Enemy1Movement()
    {
        float randomX = Random.Range(-8.5f, 7.6f);
        float randomY = Random.Range(-4.8f, 6f);

        if (transform.position.x < -9 || transform.position.y < -5 || transform.position.x > 8f || transform.position.y > 10f)
        {  
            int direction = Random.Range(1, 4);

            if (direction == 1)
            {
                transform.position = new Vector3(randomX, 7f, 0);
                moveState = MovementState.Down;
            }
            else if (direction == 2)
            {
                transform.position = new Vector3(-8.5f, randomY, 0);
                moveState = MovementState.Right;
            }
            else if(direction == 3)
            {
                transform.position = new Vector3(7.6f, randomY, 0);
                moveState = MovementState.Left;
            }
        }
        if (moveState == MovementState.Down)
        {
            transform.position += Vector3.down * _moveSpeed * Time.deltaTime; 
        }
        else if (moveState == MovementState.Left)
        {
            transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
        }
        else if (moveState == MovementState.Up)
        {
            transform.position += Vector3.up * _moveSpeed * Time.deltaTime;
        }
        else if (moveState == MovementState.Right)
        {
            transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
        }
        

        /*if (transform.position.y <= -5.3f)
        {
          float randomX = Random.Range(-8.5f, 7.6f);
          
          transform.position = new Vector3(randomX, 7f, 0);
          
          transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * _moveSpeed * Time.deltaTime; //.down = (0, -1, 0)
        }*/
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
            _audioSource.Play();
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 2.4f);
            
        }

        if (other.CompareTag("Plasma"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.Score();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _moveSpeed = 0;
            _audioSource.Play();
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 2.4f);
        }

        if (other.CompareTag ("Player"))
        {
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            player.Damage();
            _anim.SetTrigger("OnEnemyDeath");
            _moveSpeed = 0;
            _audioSource.Play();
            Destroy(GetComponent<BoxCollider2D>()); 
            Destroy(this.gameObject, 2.4f);
        }

        if (other.CompareTag("Homing"))
        {
            Destroy(other.gameObject);
            Debug.Log("Homing Shot Hit");
            if (_player != null)
            {
                _player.Score();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _moveSpeed = 0;

            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this.gameObject);
        }

    }
 
}
