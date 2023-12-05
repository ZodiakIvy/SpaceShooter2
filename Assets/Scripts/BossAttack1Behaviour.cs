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
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    

    public void SpiralLaser()
    {
        float verticalOffset = _amplitude * Mathf.Sin(Time.time * _frequency);
        transform.position = _startPosition + new Vector3(0f, verticalOffset, 0f) + transform.right * _speed * Time.deltaTime;
    }
}

