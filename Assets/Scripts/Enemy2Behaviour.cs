using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Enemy2Behaviour : MonoBehaviour
{
    private Animator _anim;

    [SerializeField]
    private bool _shieldActive = true;

    public float amplitude = 2f;

    private float _canFire = -1f;
    private float _fireRate = 3f;
    [SerializeField]
    private float _moveSpeed = 2f;

    [SerializeField]
    private GameObject _enemy2AttackPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shieldBubble1;

    private PlayerBehaviour _player;

    private Vector3 pos1;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL");
        }

        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        pos1 = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Enemy2Movement();

        EnemyHomingShot();
    }

    public enum MovementState
    {
        Down,
        Left,
        Right
    }

    public MovementState moveState = MovementState.Down; 
    //New Enemy Movement
    //Task:
    //Unique Movement Behavior(zig-zag)
    void Enemy2Movement()
    {
        

        float randomY = Random.Range(-4.8f, 6f);

        if (transform.position.x < -9 || transform.position.y < -5 || transform.position.x > 8f || transform.position.y > 10f)
        {
            int direction = Random.Range(1, 2);

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
            else if (direction == 3)
            {
                transform.position = new Vector3(0f, 9f, 0f);
                moveState = MovementState.Down;
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
        else if (moveState == MovementState.Down)
        {
            float x = amplitude * Mathf.Sin(_moveSpeed * Time.time);
            transform.position += new Vector3(x, pos1.y - _moveSpeed * Time.time, pos1.z);
        }
    }

    void EnemyHomingShot()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(1f, 3f);
            _canFire = Time.time + _fireRate;

            GameObject newEnemy2Attack = Instantiate(_enemy2AttackPrefab, transform.position + new Vector3(0, 2.5f, 0), Quaternion.identity);
            Destroy(newEnemy2Attack, 2f);

            HomingBehaviour[] lasers = newEnemy2Attack.GetComponentsInChildren<HomingBehaviour>();


            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].EnemyHoming();
            }
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

            _moveSpeed = 0;

            GameObject newEnemy2 = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(newEnemy2, 2.4f);
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser" && _shieldActive == true)
        {

            _shieldBubble1.SetActive(false);
            _shieldActive = false;

        }
        else if (other.tag == "Laser" && _shieldActive == false)
        {
            Destroy(other.gameObject);
            Debug.Log("Laser Hit");
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

        if (other.CompareTag("Plasma") && _shieldActive == true)
        {
            _shieldBubble1.SetActive(false);
            _shieldActive = false;
        }
        else if (other.CompareTag("Plasma") && _shieldActive == false)
        {
            Destroy(other.gameObject);
            Debug.Log("Plasma Shot Hit");
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

        if (other.tag == "Player" && _shieldActive == true)
        {
            _shieldBubble1.SetActive(false);
            _shieldActive = false;
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            player.Damage();
            Debug.Log("Player Collided");
        }
        else if (other.tag == "Player" && _shieldActive == false)
        {
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            player.Damage();
            Debug.Log("Player Collided");
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
