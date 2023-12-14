using UnityEngine;

public class Enemy1AttackBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;

    [SerializeField]
    private Transform _enemyTransform;
    [SerializeField]
    private Transform _playerTransform;

    void Update()
    {

        Enemy1Attack();
    }

    void Enemy1Attack()
    {
        //if (_playerTransform.position.y <= _enemyTransform.position.y)
        //{
            transform.position += (Vector3.down * _laserSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    transform.position += (Vector3.up * _laserSpeed * Time.deltaTime);
        //}
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
