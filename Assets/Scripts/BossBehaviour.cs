using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserSound;

    private bool _movingDown;
    private bool _movingRight;
    private bool _movingUp;
    private bool _ultimate;
    
    private CameraShakeBehaviour _cameraShakeBehaviour;

    private float _canFire = -1f;
    private float _fireRate = 3f;
    [SerializeField]
    private float _moveSpeed = 4;
    [SerializeField]
    private float _newSpawnDuration = .1f;
    [SerializeField]
    private float _shotgunAngle = 15;
    [SerializeField]
    private float _shotgunAngle2 = 20;
    [SerializeField]
    private float _shotgunAngle3 = 30;
    private float _overTime = 0.0f;

    [SerializeField]
    private GameObject _bossAttackPrefab;
    [SerializeField]
    private GameObject _bossAttack2Prefab;
    [SerializeField]
    private float _bossHealth;
    [SerializeField]
    private float _bossHealthFull = 500f;
    [SerializeField]
    private GameObject _bossSuper;
    [SerializeField]
    private GameObject[] _plasmaSpawnAB;
    GameObject player;
    [SerializeField]
    private GameObject[] _shotgunAB;

    private PlayerBehaviour _player;

    [SerializeField]
    private Slider _bossLife;

    [SerializeField]
    private Transform _playerTransform;
    private Transform _down; 
    private Transform _right;

    public void Start()
    {
        _cameraShakeBehaviour = GameObject.Find("Camera_Shaker").GetComponent<CameraShakeBehaviour>();
        if (_cameraShakeBehaviour != null ) 
        {
            Debug.LogError("The Camera Shaker is NULL");
        }

        _bossLife = GameObject.Find("BossHealthBar").GetComponent<Slider>();
        if (_bossLife == null)
        {
            Debug.LogError("The Thruster Bar is NULL.");
        }

        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        if (player != null)
        {
            _player = player.GetComponent<PlayerBehaviour>();
        }

        _bossHealth = _bossHealthFull;
        _bossLife.value = _bossHealth;

        transform.position = new Vector3(0, 10f, 0);

        StartCoroutine(BossMovement());
    }

        // Update is called once per frame
        public void Update()
    {
        _fireRate = Random.Range(3f, 5f);
        _canFire = Time.time + _fireRate;
        if (Time.time > _canFire)
        {
            BossAttack1();
;       }
    }

    public enum MovementState
    {
        Down,
        Right,
        Up
    }

    public MovementState moveState = MovementState.Down;

    public IEnumerator BossMovement()
    {
        if (moveState == MovementState.Down)
        {
            _movingDown = true;
            transform.position += Vector3.down * _moveSpeed * Time.deltaTime;
        }

        if (moveState == MovementState.Right)
        {
            _movingRight = true;
            transform.rotation = Quaternion.Slerp(_down.rotation, _right.rotation, _overTime);
            Quaternion target = Quaternion.Euler(0, 0, 90);
            transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
        }

        if (moveState == MovementState.Up)
        {
            _movingUp = true;
            transform.position += Vector3.up * _moveSpeed * Time.deltaTime;
        }

        moveState = MovementState.Down;
       
        while (_movingDown == true)
        {
            if (transform.position.y < 3.66f)
            {
                _moveSpeed = 0;
                BossAttack1();
                yield return new WaitForSecondsRealtime(3f);
            }
        }
        yield return null;

        _moveSpeed = 4;
        moveState = MovementState.Up;
        _movingDown = false;
        _movingUp = true;

        while (_movingUp == true)
        {
            if (transform.position.y >= 10.01f)
            {
                _moveSpeed = 0;
                BossAttack1();
            }
        }
        yield return null;

        _movingUp = false;
        transform.position = new Vector3(-10, 3, 0);
        _movingRight = true;

        while (_movingRight == true)
        {
            _moveSpeed = 4;
            yield return new WaitForSecondsRealtime(4f);
            moveState = MovementState.Right;
            Invoke(methodName: "BossAttack2", _newSpawnDuration);
            _audioSource.clip = _laserSound;
            _audioSource.Play();
            if (transform.position.x >= 10)
            {
                _moveSpeed = 0;
                _movingRight = false;
                transform.position = new Vector3(0, -10f, 0);
                yield return new WaitForSecondsRealtime(3f);
                _ultimate = true;
            }
        }
        yield return null;


        while (_ultimate == true)
        {
            _moveSpeed = 4f;
            transform.rotation = Quaternion.Slerp(_down.rotation, _right.rotation, _overTime);
            Quaternion target = Quaternion.Euler(0, 0, 180);
            moveState = MovementState.Up;
            if (transform.position.y >= -6.25f)
            {
                _moveSpeed = 0;
                BossSuper();
                yield return new WaitForSecondsRealtime(5f);
            }

        }
        yield return null;
    }

    public void BossAttack1() //Shotgun
    {
           
        for (int i = 0; i < 10; i++)
        {
            Instantiate(_bossAttackPrefab, _shotgunAB[i].transform.position, Quaternion.identity);

            Vector3 attackAngle = new Vector3(0, 0, _shotgunAngle);

            if (i == 0)
            {
                attackAngle *= -1;
            }

            Instantiate(_bossAttackPrefab, _shotgunAB[i].transform.position, Quaternion.Euler(attackAngle));

            

            Vector3 attackAngle2 = new Vector3(0, 0, _shotgunAngle2);

            if (i == 0)
            {
                attackAngle2 *= -1;
            }

            Instantiate(_bossAttackPrefab, _shotgunAB[i].transform.position, Quaternion.Euler(attackAngle2));

            Vector3 attackAngle3 = new Vector3(0, 0, _shotgunAngle3);

            if (i == 0)
            {
                attackAngle3 *= -1;
            }

            Instantiate(_bossAttackPrefab, _shotgunAB[i].transform.position, Quaternion.Euler(attackAngle3));

        }
        
        
    }

    public void BossAttack2() //PlasmaShot
    { 
        for (int i = 0; i < 2; i++)
        {
            Instantiate(_bossAttack2Prefab, _plasmaSpawnAB[i].transform.position, Quaternion.identity);

            Vector3 attackAngle = new Vector3(0, 0, _shotgunAngle);

            if (i == 0)
            {
                attackAngle *= -1;
            }

            Instantiate(_bossAttack2Prefab, _plasmaSpawnAB[i].transform.position, Quaternion.Euler(attackAngle));
        }
    }

    public void BossSuper()
    {
        _cameraShakeBehaviour.Shake();
        Instantiate(_bossSuper, transform.position + new Vector3(0, 7.5f, 0) , Quaternion.identity);

    }

    public void UpdateBossLife(float _bossHealth)
    {

        if ((_bossLife.value - _bossHealth) <= 0)
        {
            _cameraShakeBehaviour.Shake();
            //YouWin();
        }
    }

    public void Damage()
    {
        UpdateBossLife(_bossHealth--);
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
        }

        if (other.CompareTag("Plasma"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.Score();
            }
        }

        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            player.Damage();
        }

        if (other.CompareTag("Homing"))
        {
            Destroy(other.gameObject);
            Debug.Log("Homing Shot Hit");
            if (_player != null)
            {
                _player.Score();
            }
        }

    }

}
