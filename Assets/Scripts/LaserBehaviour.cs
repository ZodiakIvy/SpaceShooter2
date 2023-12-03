using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool _enemy1Attack = false;

    private float _canFire = -1f;
    private float _fireRate = 3f;
    [SerializeField]
    private float _laserSpeed = 7;

    [SerializeField]
    private Transform _enemyTransform;
    [SerializeField]
    private Transform _playerTransform;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
        }
            Laser();
    }

    public void AssignEnemyLaser()
    {
        _enemy1Attack = true;
    }

    void Laser()
    {
        if (_enemy1Attack == false)
        {
            MoveUp();
        }
        else
        {
            if (_playerTransform.position.y < _enemyTransform.position.y)
            {
                MoveDown();
            }
            else
            {
                MoveUp();
            }
        }
    }


    void MoveDown()
    {
        //translate laser down
        transform.position += (new Vector3(0, -1, 0) * _laserSpeed * Time.deltaTime);
        if (transform.position.y <= -5.3f)
        {
            if (transform.parent != null)
            { Destroy(transform.parent.gameObject); }
            Destroy(this.gameObject);
        }
    }

    void MoveUp()
    {
        //translate laser up
        transform.position += (new Vector3(0, 1, 0) * _laserSpeed * Time.deltaTime);
        if (transform.position.y >= 5.3f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _enemy1Attack == true)
            {
                PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
                if (player != null)
                { player.Damage(); }
            }
    }

}
