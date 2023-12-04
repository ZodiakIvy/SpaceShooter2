using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    private bool _stopSpawning = false;

    private BossBehaviour _bossBehaviour;

    [SerializeField]
    private float _spawnTime = 7f;
    [SerializeField]
    private float _moveSpeed = 3;

    [SerializeField]
    private GameObject _enemy1Prefab;
    [SerializeField]
    private GameObject _enemy1Level2Prefab;
    [SerializeField]
    private GameObject _enemy1Level3Prefab;
    [SerializeField]
    private GameObject _enemy2Prefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUps; //0 = TripleShot, 1 = Speed, 2 = Shield, 3 = Ammo, 4 = Health. 5 = Plasma, 6 = SpeedDebuff, 7 = HomingShot
    [SerializeField]
    private GameObject _powerUpContainer;
    
    [SerializeField]
    private int _waveCount = 1;
    
    private PlayerBehaviour _player;

    private UIManager _uiManager;

    void Start()
    {
        _bossBehaviour = GameObject.Find("BossBehaviour").GetComponent<BossBehaviour>();
        if (_bossBehaviour != null)
        {
            Debug.LogError("Boss is NULL");
        }

        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Magnet();
        }
    }

    public void Magnet()
    {
        foreach (var _powerUps in _powerUps)
        {
            _powerUps.transform.position = Vector3.MoveTowards(_powerUps.transform.position, _player.transform.position, Time.deltaTime * _moveSpeed);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void StartSpawning()
    {
        StartCoroutine(Enemy1_SpawnRoutine());
        StartCoroutine(Enemy2_SpawnRoutine());
        StartCoroutine(PowerUp_SpawnRoutine_Level1());
    }

    public void StartSpawningLevel2()
    {
        StartCoroutine(Enemy1_SpawnRoutine_Level2());
        StartCoroutine(PowerUp_SpawnRoutine_Level2());
    }

    public void StartSpawningLevel3()
    {
        StartCoroutine(Enemy1_SpawnRoutine_Level3());
        StartCoroutine(PowerUp_SpawnRoutine_Level3());
    }

    public void StartSpawningLevel4()
    {
        StartCoroutine(PowerUp_SpawnRoutine_Level4());
    }

    public void StartSpawningLevel5()
    {
        StartCoroutine(PowerUp_SpawnRoutine_Level5());
        _uiManager.transform.GetChild(9).gameObject.SetActive(true);
        StartCoroutine(BossBattle_SpawnRoutine());
    }

    IEnumerator BossBattle_SpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            _bossBehaviour.BossSuper();
            gameObject.SetActive(true);
        }
    }

    IEnumerator Enemy1_SpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            for (int i = 0; i < _waveCount; i++)
            {
                float randomX = Random.Range(-8.5f, 7.6f);
                Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);
                GameObject newEnemy1 = Instantiate(_enemy1Prefab, spawnPosition, Quaternion.identity);
                newEnemy1.transform.parent = _enemyContainer.transform;
                yield return new WaitForSecondsRealtime(2f);
            }
            yield return new WaitForSecondsRealtime(_spawnTime);
            
            if (_waveCount == 2)
            {
                StartSpawningLevel2();
                StopCoroutine(PowerUp_SpawnRoutine_Level1());
                StopCoroutine(Enemy1_SpawnRoutine());
            }
            else
            {
                Enemy1_SpawnRoutine();
            }
            _waveCount++;
        }
    }

    IEnumerator Enemy1_SpawnRoutine_Level2()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            for (int i = 0; i < _waveCount; i++)
            {
                float randomX = Random.Range(-8.5f, 7.6f);
                Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);
                GameObject newLevel2Enemy1 = Instantiate(_enemy1Level2Prefab, spawnPosition, Quaternion.identity);
                newLevel2Enemy1.transform.parent = _enemyContainer.transform;
                yield return new WaitForSecondsRealtime(2f);
            }
            yield return new WaitForSecondsRealtime(_spawnTime);

            if (_waveCount == 2)
            {
                StartSpawningLevel3();
                StopCoroutine(PowerUp_SpawnRoutine_Level2());
                StopCoroutine(Enemy1_SpawnRoutine_Level2());
            }
            else
            {
                Enemy1_SpawnRoutine_Level2();
            }
            _waveCount++;
        }
    }

    IEnumerator Enemy1_SpawnRoutine_Level3()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            for (int i = 0; i < _waveCount; i++)
            {
                float randomX = Random.Range(-8.5f, 7.6f);
                Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);
                GameObject newLevel3Enemy1 = Instantiate(_enemy1Level3Prefab, spawnPosition, Quaternion.identity);
                newLevel3Enemy1.transform.parent = _enemyContainer.transform;
                yield return new WaitForSecondsRealtime(2f);
            }
            yield return new WaitForSecondsRealtime(_spawnTime);

            if (_waveCount == 2)
            {
                StartSpawningLevel4();
                StopCoroutine(PowerUp_SpawnRoutine_Level3());
                StopCoroutine(Enemy1_SpawnRoutine_Level3());
            }
            else
            {
                Enemy1_SpawnRoutine_Level3();
            }
            _waveCount++;
        }
    }


    IEnumerator Enemy2_SpawnRoutine()
    {
        yield return new WaitForSeconds(93f);
        while (_stopSpawning == false)
        {
            for (int i = 0; i < _waveCount; i++)
            {
                float randomY = Random.Range(-4.8f, 6f);
                Vector3 spawnPosition = transform.position + new Vector3(7.6f, randomY, 0);
                GameObject newEnemy2 = Instantiate(_enemy2Prefab, spawnPosition, Quaternion.identity);
                newEnemy2.transform.parent = _enemyContainer.transform;
                yield return new WaitForSecondsRealtime(4f);
            }
            yield return new WaitForSecondsRealtime(_spawnTime);


            if (_waveCount == 2)
            {
                StartSpawningLevel5();
                StopCoroutine(PowerUp_SpawnRoutine_Level4());
                StopCoroutine(Enemy2_SpawnRoutine());
            }
            else
            {
                Enemy2_SpawnRoutine();
            }
            _waveCount++;
        }
    }

    IEnumerator PowerUp_SpawnRoutine_Level1()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);

            //1 = Speed
            GameObject newPowerUp1 = Instantiate(_powerUps[1], spawnPosition, Quaternion.identity);
            newPowerUp1.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(5f);

            //3 = Ammo
            GameObject newPowerUp3 = Instantiate(_powerUps[3], spawnPosition, Quaternion.identity);
            newPowerUp3.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //4 = Health
            GameObject newPowerUp4 = Instantiate(_powerUps[4], spawnPosition, Quaternion.identity);
            newPowerUp4.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(9f);

        }

    }

    IEnumerator PowerUp_SpawnRoutine_Level2()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);

            //0 = TripleShot
            GameObject newPowerUp = Instantiate(_powerUps[0], spawnPosition, Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(5f);

            //1 = Speed
            GameObject newPowerUp1 = Instantiate(_powerUps[1], spawnPosition, Quaternion.identity);
            newPowerUp1.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //3 = Ammo
            GameObject newPowerUp3 = Instantiate(_powerUps[3], spawnPosition, Quaternion.identity);
            newPowerUp3.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //4 = Health
            GameObject newPowerUp4 = Instantiate(_powerUps[4], spawnPosition, Quaternion.identity);
            newPowerUp4.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(5f);

            //2 = Shield
            GameObject newPowerUp2 = Instantiate(_powerUps[2], spawnPosition, Quaternion.identity);
            newPowerUp2.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(4f);

        }
    }

    IEnumerator PowerUp_SpawnRoutine_Level3()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);

            //0 = TripleShot
            GameObject newPowerUp = Instantiate(_powerUps[0], spawnPosition, Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(5f, 9f));

            //1 = Speed
            GameObject newPowerUp1 = Instantiate(_powerUps[1], spawnPosition, Quaternion.identity);
            newPowerUp1.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //3 = Ammo
            GameObject newPowerUp3 = Instantiate(_powerUps[3], spawnPosition, Quaternion.identity);
            newPowerUp3.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //4 = Health
            GameObject newPowerUp4 = Instantiate(_powerUps[4], spawnPosition, Quaternion.identity);
            newPowerUp4.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 5f));

            //2 = Shield
            GameObject newPowerUp2 = Instantiate(_powerUps[2], spawnPosition, Quaternion.identity);
            newPowerUp2.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //5 = Plasma
            GameObject newPowerUp5 = Instantiate(_powerUps[5], spawnPosition, Quaternion.identity);
            newPowerUp5.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(7f);

        }
    }

    IEnumerator PowerUp_SpawnRoutine_Level4()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);

            //0 = TripleShot
            GameObject newPowerUp = Instantiate(_powerUps[0], spawnPosition, Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(5f, 9f));

            //1 = Speed
            GameObject newPowerUp1 = Instantiate(_powerUps[1], spawnPosition, Quaternion.identity);
            newPowerUp1.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //3 = Ammo
            GameObject newPowerUp3 = Instantiate(_powerUps[3], spawnPosition, Quaternion.identity);
            newPowerUp3.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(2f, 5f));

            //4 = Health
            GameObject newPowerUp4 = Instantiate(_powerUps[4], spawnPosition, Quaternion.identity);
            newPowerUp4.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 5f));

            //2 = Shield
            GameObject newPowerUp2 = Instantiate(_powerUps[2], spawnPosition, Quaternion.identity);
            newPowerUp2.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //5 = Plasma
            GameObject newPowerUp5 = Instantiate(_powerUps[5], spawnPosition, Quaternion.identity);
            newPowerUp5.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(5f, 9f));

        }
    }

    IEnumerator PowerUp_SpawnRoutine_Level5()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);

            //0 = TripleShot
            GameObject newPowerUp = Instantiate(_powerUps[0], spawnPosition, Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(5f, 9f));

            //1 = Speed
            GameObject newPowerUp1 = Instantiate(_powerUps[1], spawnPosition, Quaternion.identity);
            newPowerUp1.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //2 = Shield
            GameObject newPowerUp2 = Instantiate(_powerUps[2], spawnPosition, Quaternion.identity);
            newPowerUp2.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //3 = Ammo
            GameObject newPowerUp3 = Instantiate(_powerUps[3], spawnPosition, Quaternion.identity);
            newPowerUp3.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));

            //4 = Health
            GameObject newPowerUp4 = Instantiate(_powerUps[4], spawnPosition, Quaternion.identity);
            newPowerUp4.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(3f, 5f));

            //5 = Plasma
            GameObject newPowerUp5 = Instantiate(_powerUps[5], spawnPosition, Quaternion.identity);
            newPowerUp5.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(5f, 9f));

            //6 = SpeedDebuff
            GameObject newPowerUp6 = Instantiate(_powerUps[6], spawnPosition, Quaternion.identity);
            newPowerUp6.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(Random.Range(2f, 5f));

            //7 = HomingShot
            GameObject newPowerUp7 = Instantiate(_powerUps[7], spawnPosition, Quaternion.identity);
            newPowerUp7.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSecondsRealtime(9f);
        }

    }
 
}
