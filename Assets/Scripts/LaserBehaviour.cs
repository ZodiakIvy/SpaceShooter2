using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;
    [SerializeField]
    private GameObject _laser;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
