using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleshotBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3;

    [SerializeField]
    private GameObject _tripleshotPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (new Vector3(0, -1, 0) * _moveSpeed * Time.deltaTime);
        if (transform.position.y <= -5.3f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();
            other.transform.GetComponent<PlayerBehaviour>().TripleShotActive();
            Destroy(this.gameObject);
        }
    }

}
