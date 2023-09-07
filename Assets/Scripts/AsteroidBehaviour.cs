using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 19f;
    [SerializeField]
    private float _moveSpeed = .5f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private Animator _anim;
    private  PlayerBehaviour _player;
    private SpawnManager _spawnManager;
    


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
        
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    void AsteroidMovement()
    {
        transform.Rotate(new Vector3(0, 0, 1) * _rotateSpeed * Time.deltaTime);
        transform.position += (new Vector3(1, 0, 0) * _moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);
        if (other.tag == "Laser")
        {
            
            Destroy(other.gameObject);
            _moveSpeed = 0;
            GameObject newAsteroid = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(newAsteroid, 2.4f);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);
           
        }

        if (other.tag == "Player")
        {
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            player.Damage();
            _moveSpeed = 0;
            GameObject newAsteroid = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(newAsteroid, 2.4f);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);
        }


    }
}
