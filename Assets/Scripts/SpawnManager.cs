using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject _plasmaShot;
    [SerializeField]
    private GameObject _enemy1Prefab;
    [SerializeField] 
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(Enemy1_SpawnRoutine());
        StartCoroutine(PowerUp_SpawnRoutine());
    }

    IEnumerator Enemy1_SpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
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
            int randomPowerUp = Random.Range(0, 5);
            
            GameObject newPowerUp = Instantiate(_powerUps[randomPowerUp], spawnPosition, Quaternion.identity);
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));
            
            GameObject newAmmoType = Instantiate(_plasmaShot, spawnPosition, Quaternion.identity);
            yield return new WaitForSecondsRealtime(20f);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
