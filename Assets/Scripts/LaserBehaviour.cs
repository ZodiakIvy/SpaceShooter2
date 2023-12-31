using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool _enemy1Attack = false;

    [SerializeField]
    private float _laserSpeed = 7;

    // Update is called once per frame
    void Update()
    {
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
            MoveDown();
        }
   
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

        if (other.tag == "Boss")
        {
            BossBehaviour boss = other.GetComponent<BossBehaviour>();
            if (boss != null)
            { boss.Damage(); }
        }
    }

}
