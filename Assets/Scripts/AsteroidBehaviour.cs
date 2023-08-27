using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _asteroid;
    [SerializeField]
    private float _speed = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    void AsteroidMovement()
    {
        transform.Rotate(new Vector3(0, 0, 3) * _speed * Time.deltaTime); 
        if (transform.position.y <= -5.3f)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            float randomY = Random.Range(-3.3f, 7f);
            Vector3 asteroidPosition = transform.position + new Vector3(randomX, randomY, 0);
            GameObject newAsteroid = Instantiate(_asteroid, asteroidPosition, Quaternion.Euler(0f, 0f, 3f));
        }
    }
}
