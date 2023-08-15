using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3;

    [SerializeField]
    private GameObject _tripleshotPrefab;
    [SerializeField]
    private GameObject _speedPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private int _powerupID;
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

            if (player != null) 
            {
                if (_powerupID == 0) 
                { player.TripleShotActive(); }
                else if (_powerupID == 1) 
                { player.SpeedActive(); }
                else if (_powerupID == 2) 
                { player.ShieldActive(); }
                
            }

            Destroy(this.gameObject);
        }
    }

}
