using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Enemy2Behaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy2AttackPrefab;
    [SerializeField]
    private float _moveSpeed = 2f;
    private float _fireRate = 3f;
    private float _canFire = -1f;
    public float amplitude = 2f;
    private Vector3 pos1;
    [SerializeField]
    private GameObject _explosionPrefab;
    private Animator _anim;
    private PlayerBehaviour _player;
    [SerializeField]
    private bool _shieldActive = true;
    [SerializeField]
    private GameObject _shieldBubble1;
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

        pos1 = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Enemy2Movement();

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

    public enum MovementState
    {
        Right,
        Left,
        Down
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);
        if (other.tag == "Laser" && _shieldActive == true)
        {

            _shieldBubble1.SetActive(false);
            _shieldActive = false;

        }
        else
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

        if (other.tag == "Player" && _shieldActive == true)
        {
            _shieldBubble1.SetActive(false);
            _shieldActive = false;
        }
        else 
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

        if (other.CompareTag("Plasma") && _shieldActive == true)
        {
            _shieldBubble1.SetActive(false);
            _shieldActive = false;
        }
        else 
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
    }
}
