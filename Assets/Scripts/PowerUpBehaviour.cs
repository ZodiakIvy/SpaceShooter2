﻿using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3;
    [SerializeField]
    private int _powerUps; //0 = TripleShot, 1 = Speed, 2 = Shield, 3 = Ammo, 4 = Health, 5 = PlasmaShot, 6 = SpeedDebuff, 7 = Homing
    [SerializeField]
    private AudioClip _clip;
    [SerializeField]
    private Rigidbody2D _playerRigidbody;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _powerUpTransform;

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
        if (other.CompareTag ("Player"))
        {
            
            PlayerBehaviour player = other.transform.GetComponent<PlayerBehaviour>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player != null)
            { 
                switch(_powerUps) 
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedActive(); 
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.MoreBullets();
                        break;
                    case 4:
                        player.HealthUp();
                        break;
                    case 5:
                        player.PlasmaShotActive();
                        break;
                    case 6:
                        player.SpeedDebuffActive();
                        break;
                    case 7:
                        player.HomingShotActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }

}
