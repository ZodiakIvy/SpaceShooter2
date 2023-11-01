using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool _enemy2Attack = false;
    [SerializeField]
    private float _laserSpeed = 6f;
    [SerializeField]
    private float _rotateSpeed = 200f;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Rigidbody2D _homingShot;

    // Start is called before the first frame update
    void Start()
    {
        _homingShot = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Homing();
    }

    void Homing()
    {
        if (_enemy2Attack == false)
        {
            Vector2 direction = (Vector2)_target.position - _homingShot.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            _homingShot.angularVelocity = -rotateAmount * _rotateSpeed;

            _homingShot.velocity = transform.up * _laserSpeed;

            if (transform.position.x < -10 || transform.position.y < -10 || transform.position.x > 10f || transform.position.y > 10f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
        else 
        { 
        
        }
    }

    public void EnemyHoming()
    {
        _enemy2Attack = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _enemy2Attack == true)
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            { player.Damage(); }
        }
    }
}
