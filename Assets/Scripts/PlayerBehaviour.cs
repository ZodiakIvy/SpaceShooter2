using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private GameObject _Laser;
    public float fireRate = .25f;
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _maxHealth = 100f;
    public float currentHealth = Mathf.Clamp(100f, 0f, 100f);
    [SerializeField]
    private float _maxShield = Mathf.Clamp(100f, 0f, 100f);
    public float currentShield = Mathf.Clamp(100f, 0f, 100f);
    public Image healthBar;
    public Image shieldBar;
    public Image healthBubble;
    public Image shieldBubble;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager != null )
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
    }
    void FireLaser()
    {
        _canFire = Time.time + fireRate;
        Instantiate(_Laser, transform.position + new Vector3(0, .75f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        currentShield = currentShield - 25;

        if (currentShield <= 0)
        {
            currentShield = 0;
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
    void FillHealth(float healthIn)
    {
        float addHealth = _maxHealth * healthIn;
        currentHealth += addHealth;
        if (currentHealth > _maxHealth)
        {
            currentHealth = _maxHealth;
        }
    }
    void HealthUp(float valueUp)
    {
        _maxHealth += valueUp;
        currentHealth += valueUp;
        healthBar.fillAmount = (float)currentHealth / (float)_maxHealth;
        if(healthBubble != null)
        {
            healthBubble.fillAmount = (float)currentHealth / (float)_maxHealth;
        }
    }

    void FillShield(float shieldIn)
    {
        float addShield = _maxShield * shieldIn;
        currentShield += addShield;
        if (currentShield > _maxShield)
        {
            currentShield = _maxShield;
        }
    }
    void ShieldUp(float valueUp)
    {
        _maxShield += valueUp;
        currentShield += valueUp;
        shieldBar.fillAmount = (float)currentShield / (float)_maxShield;
        if (shieldBubble != null)
        {
            shieldBubble.fillAmount = (float)currentShield / (float)_maxShield;
        }
    }

    void Health(float healthIn)
    {
        currentHealth += healthIn;
        healthBar.fillAmount = (float)currentHealth / (float)_maxHealth;
        if (healthBubble != null)
        {
            healthBubble.fillAmount = (float)currentHealth / (float)_maxHealth;
        }
    }

    void Shield(float shieldIn)
    {
        currentShield += shieldIn;
        shieldBar.fillAmount = (float)currentShield / (float)_maxShield;
        if (shieldBubble != null)
        {
            shieldBubble.fillAmount = (float)currentShield / (float)_maxShield;
        }
    }
}
