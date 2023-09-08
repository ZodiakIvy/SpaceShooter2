using UnityEngine;


public class Enemy1Behaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy1AttackPrefab;
    private float _fireRate = 3f;
    private float _canFire = -1f;
    [SerializeField]
    private float _moveSpeed = 4;
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
        Enemy1Movement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;

            GameObject newEnemy1Attack = Instantiate(_enemy1AttackPrefab, transform.position + new Vector3(0, -.75f, 0), Quaternion.identity);

            LaserBehaviour[] lasers = newEnemy1Attack.GetComponentsInChildren<LaserBehaviour>();


            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
<<<<<<< HEAD

=======
>>>>>>> bc922f14701ad25fc66295d5799fea60dc902124
        }

    }

    void Enemy1Movement()
    {
        transform.position += (new Vector3(0, -1, 0) * _moveSpeed * Time.deltaTime);
        if (transform.position.y <= -5.3f)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);
        if (other.CompareTag ("Laser"))
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

        if (other.CompareTag ("Player"))
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
