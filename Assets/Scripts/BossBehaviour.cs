using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private CameraShakeBehaviour _cameraShakeBehaviour;

    private float _canFire = -1f;
    private float _fireRate = 3f;
    [SerializeField]
    private float _moveSpeed = 4;
    [SerializeField]
    private float _shotgunAngle = 15;

    [SerializeField]
    private GameObject _bossAttackPrefab;
    [SerializeField]
    private GameObject _enemy2AttackPrefab;
    [SerializeField]
    private GameObject[] _shotgunAB;

    [SerializeField]
    private Transform _playerTransform;
 
    public void Start()
    {
        _cameraShakeBehaviour = GameObject.Find("Camera_Shaker").GetComponent<CameraShakeBehaviour>();
        StartCoroutine(BossMovement());
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public enum MovementState
    {
        Down,
        Up
    }

    public MovementState moveState = MovementState.Down;

    public IEnumerator BossMovement()
    {
        transform.position = new Vector3(0, 10f, 0);
        moveState = MovementState.Down;
        while (true)
        {
            if (transform.position.y < 3.66f)
            {
                _moveSpeed = 0;
                BossAttack1();
                yield return new WaitForSecondsRealtime(4f);
                moveState = MovementState.Up;


                if (transform.position.y >= 10.01f)
                {
                    _moveSpeed = 0;
                    BossAttack2();
                    yield return new WaitForSecondsRealtime(5f);
                    moveState = MovementState.Down;
                }

                yield return new WaitForSecondsRealtime(15f);
                BossSuper();
            }

            if (moveState == MovementState.Down)
            {
                transform.position += Vector3.down * _moveSpeed * Time.deltaTime;
            }

            if (moveState == MovementState.Up)
            {
                transform.position += Vector3.up * _moveSpeed * Time.deltaTime;
            }

            yield return null;
        }
        
    }

    public void BossAttack1() //Shotgun
    {
           
        for (int i = 0; i < 2; i++)
        {
            GameObject bossAttack1 = Instantiate(_bossAttackPrefab, _shotgunAB[i].transform.position, Quaternion.identity);

            LaserBehaviour[] lasers = bossAttack1.GetComponentsInChildren<LaserBehaviour>();


            for (int j = 0; j < lasers.Length; j++)
            {
                lasers[j].AssignEnemyLaser();
            }

            Vector3 attackAngle = new Vector3(0, 0, _shotgunAngle);

            if (i == 1)
            {
                attackAngle *= -1;
            }

            bossAttack1 = Instantiate(_bossAttackPrefab, _shotgunAB[i].transform.position, Quaternion.Euler(attackAngle));

            lasers = bossAttack1.GetComponentsInChildren<LaserBehaviour>();


            for (int j = 0; j < lasers.Length; j++)
            {
                lasers[j].AssignEnemyLaser();
            }


        }
        
        
    }

    public void BossAttack2()
    {
        if (Time.time > _canFire)
        {
            _fireRate = .25f;
            _canFire = Time.time + _fireRate;

            GameObject bossAttack2 = Instantiate(_enemy2AttackPrefab, transform.position + new Vector3(0, 2.5f, 0), Quaternion.identity);
            Destroy(bossAttack2, 4f);

            HomingBehaviour[] lasers = bossAttack2.GetComponentsInChildren<HomingBehaviour>();


            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].EnemyHoming();
            }
        }
    }

    public void BossSuper()
    {
        _cameraShakeBehaviour.Shake();
    }




}
