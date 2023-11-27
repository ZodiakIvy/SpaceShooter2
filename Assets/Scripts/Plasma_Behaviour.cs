using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma_Behaviour : MonoBehaviour
{
    [SerializeField]
    private float _plasmaSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        Plasma();
    }

    void Plasma()
    {
            MoveUp(); 
    }


    void MoveUp()
    {
        //translate laser up
        transform.position += (new Vector3(0, 1, 0) * _plasmaSpeed * Time.deltaTime);
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
