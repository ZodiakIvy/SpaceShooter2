using UnityEngine;

public class Enemy1AttackBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;

    // Update is called once per frame
    void Update()
    {
        Enemy1Attack();
    }

    void Enemy1Attack()
    {
        MoveDown();
    }
    void MoveDown()
    {
        //translate laser down
        transform.position += (new Vector3(0, -1, 0) * _laserSpeed * Time.deltaTime);
        if (transform.position.y <= -5.3f)
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

