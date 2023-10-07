using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _powerUps; //0 = TripleShot, 1 = Speed, 2 = Shield, 3 = Ammo, 4 = Health. 5 = Plasma
    [SerializeField]
    private GameObject[] _ammoType; //0 = Laser, 1 = TripleShot, 2 = Plasma, 3 = HomingShot
    [SerializeField]
    private GameObject _enemy1Prefab;
    [SerializeField] 
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(Enemy1_SpawnRoutine());
        StartCoroutine(PowerUp_SpawnRoutine());
        //StartCoroutine(Ammo_SpawnRoutine());
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
            int randomPowerUp = Random.Range(0, 6);

            GameObject newPowerUp = Instantiate(_powerUps[randomPowerUp], spawnPosition, Quaternion.identity);
            yield return new WaitForSecondsRealtime(Random.Range(3f, 7f));
        }
    }

    IEnumerator Ammo_SpawnRoutine()
    {
        while (_stopSpawning == false) 
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 9, 0);
            int randomAmmoType = Random.Range(0, 2);

            GameObject newAmmoType = Instantiate(_ammoType[randomAmmoType], spawnPosition, Quaternion.identity);
            yield return new WaitForSecondsRealtime(20f);
        }
    }
    
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
