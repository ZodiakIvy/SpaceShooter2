using UnityEngine;

public class Enemy1AttackBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _enemyTransform;
    [SerializeField]
    private float _laserSpeed = 8f;
    [SerializeField]
    private Transform[] _powerUpTransform;
    [SerializeField]
    private float _rammingDistance = 2f;
    [SerializeField]
    private GameObject _enemy1AttackPrefab;
    private float _fireRate = 3f;
    private float _canFire = -1f;

    void Update()
    {
        PowerUpShot();

        if (_playerTransform.position.y < _enemyTransform.position.y)
        {
            transform.position += (Vector3.down * _laserSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += (Vector3.up * _laserSpeed * Time.deltaTime);
        }

        if (transform.position.y <= -5.3f || transform.position.y >= 7f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void PowerUpShot()
    {
        int[] x = { 0, 1, 2, 3, 4, 5, 6, 7 };
        float powerUpdistance = Vector3.Distance(transform.position, _powerUpTransform[x[0]].transform.position);

        if (powerUpdistance < _rammingDistance && Time.time > _canFire)
        {
            _fireRate = .5f;
            _canFire = Time.time + _fireRate;

            GameObject newEnemy1Attack = Instantiate(_enemy1AttackPrefab, transform.position + new Vector3(0, -2.75f, 0), Quaternion.identity);

            LaserBehaviour[] lasers = newEnemy1Attack.GetComponentsInChildren<LaserBehaviour>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();

            if (player != null)
            {
                player.Damage();
            }
        }
    }
}
