using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBehaviour : MonoBehaviour
    //speed cariable of 8
{
    public float LaserSpeed = 8f;
    [SerializeField]
    private GameObject _Laser;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up
        transform.position += (new Vector3(0, 1, 0) * LaserSpeed * Time.deltaTime);
        // once the position of the laser goes beyond the boundary of the scene then destroy the laser
        if (transform.position.y >= 5.3f)
        {
            Destroy(_Laser);
        }
    }
}
