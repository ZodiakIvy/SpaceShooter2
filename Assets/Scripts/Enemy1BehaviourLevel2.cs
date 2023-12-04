using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1BehaviourLevel2 : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private AudioClip _explosion_sound;

    private AudioSource _audioSource;

    private bool isDodging = false;

    private float _canFire = -1f;
    public float dodgeDelay = 0.5f;
    public float dodgeDirection = 1f;
    public float dodgeDistance = 3f;
    public float dodgeSpeed = 10f;
    public float dodgeTime = 0.5f;
    private float _fireRate = 3f;
    [SerializeField]
    private float _moveSpeed = 4;

    [SerializeField]
    private GameObject _enemy1AttackPrefab;
    
    private PlayerBehaviour _player;
    
    [SerializeField]
    private Rigidbody2D _enemyRigidbody;
    
    private Vector3 dodgeTarget;


    // Start is called before the first frame update
    void Start()
    {
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

        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Level2Enemy1Movement();

        Enemy1Attack();

        DodgeMethod();

    }

    public enum MovementState
    {
        Down,
        Left,
        Right
    }

    public MovementState moveState = MovementState.Down;
    
    void Enemy1Attack()
    {
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
    
    void DodgeMethod()
    {
        if (isDodging)
        {
            transform.position = Vector3.MoveTowards(transform.position, dodgeTarget, dodgeSpeed * Time.deltaTime);
            if (transform.position == dodgeTarget)
            {
                isDodging = false;
                Invoke("ResetDodge", dodgeDelay);
            }
        }
    }

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
                moveState = MovementState.Left;
            }
            else if (direction == 3)
            {
                transform.position = new Vector3(7.6f, randomY, 0);
                moveState = MovementState.Right;
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

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.Score();
            }

            if (other.CompareTag("Laser"))
            {
                Vector3 direction = transform.position - other.transform.position;
                if (Mathf.Abs(direction.x) < 2f)
                {
                    isDodging = true;
                    dodgeTarget = transform.position + new Vector3(dodgeDistance * dodgeDirection, 0f, 0f);
                    dodgeDirection *= -1f;
                }
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

        

    }
}
