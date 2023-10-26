using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Enemy2Behaviour : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = .5f;
    [SerializeField]
    private GameObject _explosionPrefab;
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
        Enemy2Movement();
    }

    public enum MovementState
    {
        Right,
        Left
    }

    public MovementState moveState = MovementState.Left;
    //New Enemy Movement
    //Task:
    //Unique Movement Behavior(zig-zag)
    void Enemy2Movement()
    {
        float randomY = Random.Range(-4.8f, 6f);

        if (transform.position.x < -9 || transform.position.y < -5 || transform.position.x > 8f || transform.position.y > 10f)
        {
            int direction = Random.Range(1, 4);

            if (direction == 1)
            {
                transform.position = new Vector3(-8.5f, randomY, 0);
                moveState = MovementState.Right;
            }
            else if (direction == 2)
            {
                transform.position = new Vector3(7.6f, randomY, 0);
                moveState = MovementState.Left;
            }
        }
        if (moveState == MovementState.Left)
        {
            transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
        }
        else if (moveState == MovementState.Right)
        {
            transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
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

            _moveSpeed = 0;
            Destroy(GetComponent<BoxCollider2D>());
            GameObject newEnemy2 = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(newEnemy2, 2.4f);
            Destroy(this.gameObject);

        }

        if (other.tag == "Player")
        {
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            player.Damage();

            if (_player != null)
            {
                _player.Score();
            }

            _moveSpeed = 0;
            Destroy(GetComponent<BoxCollider2D>());
            GameObject newEnemy2 = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(newEnemy2, 2.4f);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Plasma"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.Score();
            }

            _moveSpeed = 0;
            Destroy(GetComponent<BoxCollider2D>());
            GameObject newEnemy2 = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(newEnemy2, 2.4f);
            Destroy(this.gameObject);
        }

    }
}
