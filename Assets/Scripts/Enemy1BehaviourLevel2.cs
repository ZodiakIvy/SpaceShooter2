using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1BehaviourLevel2 : MonoBehaviour
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
        Level2Enemy1Movement();

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
        Right,
        Down,
        Left
    }

    public MovementState moveState = MovementState.Down;
    void Level2Enemy1Movement()
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
            else if (direction == 3)
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
        else if (moveState == MovementState.Right)
        {
            transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
        }

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);
        if (other.CompareTag("Laser"))
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

        if (other.CompareTag("Player"))
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
