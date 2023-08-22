using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    private int _maxShield = 50;
    public int currentShield = Mathf.Clamp(0, 0, 50);
    [SerializeField]
    private GameObject _shieldBubble;
    [SerializeField]
    private bool _shieldActive;
    private SpawnManager _spawnManager;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    private GameManager _gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_spawnManager == null )
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null )
        {
            Debug.LogError("The UI Manager is null.");
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
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        if (_shieldActive == true)
        {
            currentShield = _maxShield;
        }
    }

    public void Damage()
    {
        if (_shieldActive == true)
        {
            _shieldActive = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            currentShield = currentShield - 50;
            return;
        }


        if (_shieldActive == false)
        {
            _lives--;
            _uiManager.UpdateLives(_lives);
        }

        if(_lives < 1)
        {
            Destroy(this.gameObject);
            GameOverSequence();
            _spawnManager.OnPlayerDeath();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _uiManager.transform.GetChild(2).gameObject.SetActive(true);
        _uiManager.transform.GetChild(3).gameObject.SetActive(true);
    }
    public void Score()
    {
        _score += 10;
        _uiManager.Enemy1Hit(_score);

    }


    }
