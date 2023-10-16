using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    private bool _speedActive;
    [SerializeField]
    private Slider _thrusterGauge;
    [SerializeField]
    private bool _isThrusting = false;
    [SerializeField]
    private float _gasTankFull = 100f;
    [SerializeField]
    private float _gasTank;
    [SerializeField]
    private float _thrusterRefillRate = 20f;
    [SerializeField]
    private int _ammo = 15;
    private float _canFire = 0.0f;
    [SerializeField]
    private GameObject[] _ammoType; //1 = _laser, 2 = _tripleShot, 3 = _plasma, 4 = _homing
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _plasmaShot;
    [SerializeField]
    private bool _tripleshotActive;
    [SerializeField] 
    private bool _plasmashotActive;
    [SerializeField]
    private float _newSpawnDuration = .1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    [SerializeField]
    private GameObject _shieldBubble3;
    [SerializeField]
    private GameObject _shieldBubble2;
    [SerializeField]
    private GameObject _shieldBubble1;
    [SerializeField]
    private bool _shieldActive;
    [SerializeField]
    private int _shieldHealth;
    private SpawnManager _spawnManager;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    private GameManager _gameManager;
    [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private AudioClip _noShot;
    private AudioSource _audioSource;
    private CameraShakeBehaviour _cameraShakeBehaviour;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _cameraShakeBehaviour = GameObject.Find("Camera_Shaker").GetComponent<CameraShakeBehaviour>();
        _audioSource = GetComponent<AudioSource>();
        _thrusterGauge = GameObject.Find("Thruster_Gauge").GetComponent<Slider>();
        _gasTank = _gasTankFull;
        _thrusterGauge.value = _gasTank;

        if (_thrusterGauge == null)
        {
            Debug.LogError("The Thruster Bar is NULL.");
        }

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

       if (_thrusterGauge == null)
        {
            Debug.LogError("The Slider is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the Player is NULL");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _plasmashotActive == false)
        {
            if (_ammo >= 1)
            {
                FireLaser();
                _ammo--;
                _audioSource.Play();
                _uiManager.ShotsFired(_ammo);
            }
            else if (_ammo <= 0)
            {
                _ammo = 0;
                _audioSource.clip = _noShot;
                _audioSource.Play();
            }
        }
        else if (Input.GetKey(KeyCode.Space) && Time.time > _canFire && _plasmashotActive == true)
        {
            FiringPlasma();
        }

        if (_gasTank < _gasTankFull)
        {
            _gasTank += _thrusterRefillRate * Time.deltaTime;
            _gasTank = Mathf.Clamp(_gasTank, 0f, _gasTankFull);
            _thrusterGauge.value = _gasTank;
        }
        

    }
    void PlayerMovement()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && _thrusterGauge.value > 2)
        {
            IsThrusting();
            StartCoroutine(ThrusterRoutine());
        }
    

        if (Input.GetKeyUp(KeyCode.LeftShift) || _thrusterGauge.value < 1)
        {
            StopThrusting();
            StartCoroutine(ThrusterRefillRoutine());
        }


        if (_speedActive == true)
        {
            transform.Translate(new Vector3(1, 0, 0) * _horizontalInput * (_speed * _speedMultiplier) * Time.deltaTime);
            transform.Translate(new Vector3(0, 1, 0) * _verticalInput * (_speed * _speedMultiplier) * Time.deltaTime);
        }
        else if (_speedActive == false)
        {
            transform.Translate(new Vector3(1, 0, 0) * _horizontalInput * _speed * Time.deltaTime);
            transform.Translate(new Vector3(0, 1, 0) * _verticalInput * _speed * Time.deltaTime);
        }



        if (transform.position.x >= 9.15f)
        {
            transform.position = new Vector3(-9.15f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.15f)
        {
            transform.position = new Vector3(9.15f, transform.position.y, 0);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 3f), 0);
    }
    
    public void UpdateGasTank(float _gasTank)
    {
        if ((_thrusterGauge.value - _gasTank) <= 0)
        {
            StopThrusting();
        }
        
        _thrusterGauge.value += _gasTank; 
        
    }

    public void IsThrusting()
    {
        _isThrusting = true;
        if (_isThrusting == true)
        {
            UpdateGasTank(-2);
            _speed = _speed + 4f;
        }
    }

    public void StopThrusting()
    {
        _isThrusting = false;
        _speed = 5f;
        
    }

    IEnumerator ThrusterRoutine()
    { 
        while (_isThrusting == true) 
        { 
            UpdateGasTank(-20); 
            yield return new WaitForSeconds(1.0f); 
        } 
    }

    IEnumerator ThrusterRefillRoutine()
    { 
        while (_isThrusting == false) 
        { 
            yield return new WaitForSeconds(3.0f);
            UpdateGasTank(+20);
        } 
    }

    void FireLaser()
    {
        if (_ammo > 0 && _tripleshotActive == true)
        {
            Instantiate(_tripleShot, transform.position + new Vector3(-.16f, .75f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, transform.position + new Vector3(-.16f, .75f, 0), Quaternion.identity);
        }
    }

    public void TripleShotActive()
    {
        _tripleshotActive = true;
        StartCoroutine(PowerDown0());
    }

    void FiringPlasma()
    {
        Invoke(methodName: "FirePlasma", _newSpawnDuration);
        _audioSource.clip = _laserSound;
        _audioSource.Play();
    }

    void FirePlasma()
    {
        if (_plasmashotActive == true) 
        { 
            Instantiate(_plasmaShot, transform.position + new Vector3(-.16f, .75f, 0), Quaternion.identity);
        }
    }

    public void PlasmaShotActive()
    {
        _plasmashotActive = true;
        _uiManager.transform.GetChild(4).gameObject.SetActive(true);
        StartCoroutine(PowerDown2());
    }
    public void SpeedActive()
    {
        _speedActive = true;
        StartCoroutine(PowerDown1());
    }
    IEnumerator PowerDown0()
    {
        yield return new WaitForSeconds(5f);
        _tripleshotActive = false;
    }
    IEnumerator PowerDown1()
    {
        yield return new WaitForSeconds(5f);
        _speedActive = false;
    }

    IEnumerator PowerDown2()
    {
        yield return new WaitForSeconds(5f);
        _plasmashotActive = false;
        _uiManager.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void ShieldActive()
    {
        _shieldActive = true;
        _shieldHealth = 3;
        _shieldBubble3.SetActive(true);
    }

    public void MoreBullets()
    {
        _ammo += 15;
        _audioSource.clip = _laserSound;
    }

    public void Damage()
    {
        //track the shields lives with a field variable 
        if (_shieldActive == true)
        {
            _shieldHealth--;
            switch (_shieldHealth)
            {
                case 2:
                    _shieldBubble3.SetActive(false);
                    _shieldBubble2.SetActive(true);
                    break;
                case 1:
                    _shieldBubble2.SetActive(false);
                    _shieldBubble1.SetActive(true);
                    break;
                case 0:
                    _shieldBubble1.SetActive(false);
                    _shieldActive = false;
                    break;
            }
            return;
        }

        _cameraShakeBehaviour.Shake();

        if (_shieldActive == false)
        {
            _lives--;
            _uiManager.UpdateLives(_lives);
            if (_lives == 2)
            {
                _leftEngine.SetActive(true);
            }
            else if (_lives == 1)
            {
                _rightEngine.SetActive(true);
            }
            //int randomDamage = Random.Range(2, 5);
            //gameObject.transform.GetChild(randomDamage).gameObject.SetActive(true);
        }

        if (_lives < 1)
        {
            Destroy(this.gameObject);
            GameOverSequence();
            _spawnManager.OnPlayerDeath();
        }
    }

    public void HealthUp()
    {
        _lives++;
        _uiManager.UpdateLives(_lives);
        if (_lives >= 3)
        {
            _lives = 3;
            _uiManager.UpdateLives(3);
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
