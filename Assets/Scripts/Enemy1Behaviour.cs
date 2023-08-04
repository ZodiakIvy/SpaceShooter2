using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1Behaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _Enemy1;
    [SerializeField]
    private GameObject _Laser;
    [SerializeField]
    private GameObject _Player;
    [SerializeField]
    private float _MoveSpeed = 4;
 
    // Start is called before the first frame update
    void Start()
    {
        {
            transform.position = new Vector3(0, 9, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Enemy1Movement();
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject); 
            Destroy(this.gameObject);
        }

        if (other.tag == "Player")
        {
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            other.transform.GetComponent<PlayerBehaviour>().Damage();
            Destroy(this.gameObject);
        }

 
                
    }
    void Enemy1Movement()
    {
        transform.position += (new Vector3(0, -1, 0) * _MoveSpeed * Time.deltaTime);
        if (transform.position.y <= -5.3f)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }
}
