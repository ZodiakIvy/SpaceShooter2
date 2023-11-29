using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1Behaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    [SerializeField]
    private float _amplitude = 1;
    [SerializeField]
    private float _frequency = 2;
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
        for (int i = 0; i < 4; i++)
        {
            ShotGun();
        }
    }

    public void ShotGun()
    {
        float randomX = Random.Range(240, 330);
        float randomY = Random.Range(-.7f, -1f);
        transform.position += (new Vector3(randomX, randomY, 0) * _speed * Time.deltaTime);
        if (transform.position.y >= 5.3f)
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
        transform.position = _startPosition + new Vector3(0f, verticalOffset, 0f) + transform.right * _speed * Time.deltaTime;
    }
}

