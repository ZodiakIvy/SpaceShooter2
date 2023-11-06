using UnityEngine;

public class Enemy1AttackBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _enemyTransform;
    [SerializeField]
    private float _laserSpeed = 8f;

    void Update()
    {
        if (_playerTransform.position.y < _enemyTransform.position.y)
        {
            transform.position += (new Vector3(0, -1, 0) * _laserSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += (new Vector3(0, 1, 0) * _laserSpeed * Time.deltaTime);
        }

        if (transform.position.y <= -5.3f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
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
