using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private bool _speedActive;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _fireRate = .25f;
    private float _canFire = 0.0f;
    [SerializeField]
    private GameObject _tripleshot;
    [SerializeField]
    private bool _tripleshotActive;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _maxHealth = 100f;
    public float currentHealth = Mathf.Clamp(100f, 0f, 100f);
    [SerializeField]
    private float _maxShield = 100f;
    public float currentShield = Mathf.Clamp(100f, 0f, 100f);
    [SerializeField]
    private bool _shieldActive;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null )
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }
    void PlayerMovement ()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(1, 0, 0) * _horizontalInput * _speed * Time.deltaTime);
        transform.Translate(new Vector3(0, 1, 0) * _verticalInput * _speed * Time.deltaTime);

        if (transform.position.x >= 9.15f)
        {
            transform.position = new Vector3(-9.15f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.15f)
        {
            transform.position = new Vector3(9.15f, transform.position.y, 0);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 3f), 0);
        if (_speedActive == false)
        { _speed = 5; }
        else if (_speedActive == true) 
        { _speed = 10; }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        
        if (_tripleshotActive == true)
        {
            Instantiate(_tripleshot, transform.position + new Vector3(-.16f, 0, 0), Quaternion.identity);
        }
        else Instantiate(_laser, transform.position + new Vector3(0, .75f, 0), Quaternion.identity);
    }

    public void TripleShotActive()
    {
        _tripleshotActive = true;
        StartCoroutine(PowerDown());
    }

    public void SpeedActive()
    {
        _speedActive = true;
        StartCoroutine(PowerDown());
    }
    IEnumerator PowerDown()
    {
        yield return new WaitForSecondsRealtime(5f);
        _tripleshotActive = false;
        yield return new WaitForSecondsRealtime(5f);
        _speedActive = false;

    }

    public void ShieldActive() 
    {
        _shieldActive = true;
        if (_shieldActive == true)
        {
            currentShield = _maxShield;
        }
    }

    public void Damage()
    {
        currentShield = currentShield - 25f;

        if (currentShield < 1)
        {
            _shieldActive = false;
            currentHealth = currentHealth - 25;
        }

        if (currentHealth == 0)
        {
            _lives--; 
            currentHealth = _maxHealth;
        }

        if (_lives < 1)
        {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }
}
