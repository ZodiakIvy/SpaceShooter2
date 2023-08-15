using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject _enemy1Prefab;
    [SerializeField] 
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Enemy1_SpawnRoutine());
        StartCoroutine(PowerUp_SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Enemy1_SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);
            GameObject newEnemy1 = Instantiate(_enemy1Prefab, spawnPosition, Quaternion.identity);
            newEnemy1.transform.parent = _enemyContainer.transform;
            yield return new WaitForSecondsRealtime(5f);
        }
    }

    IEnumerator PowerUp_SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject newPowerUp = Instantiate(_powerUps[randomPowerUp], spawnPosition, Quaternion.identity);
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
