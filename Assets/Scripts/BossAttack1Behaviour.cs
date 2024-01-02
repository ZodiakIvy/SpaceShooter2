using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1Behaviour : MonoBehaviour
{
    [SerializeField]
    private float _amplitude = 1;
    [SerializeField]
    private float _frequency = 2;
    [SerializeField]
    private float _speed = 4;

    
    
    [SerializeField]
    private Vector3 _startPosition;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown();
        
    }

    
    public void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.x < -10 || transform.position.y < -10 || transform.position.x > 10f || transform.position.y > 10f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
    public void SpiralLaser()
    {
        float verticalOffset = _amplitude * Mathf.Sin(Time.time * _frequency);
        transform.position += _startPosition + new Vector3(0f, verticalOffset, 0f) + transform.right * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            player.Damage();
            _speed = 0;
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(this.gameObject);
        }
    }
}

