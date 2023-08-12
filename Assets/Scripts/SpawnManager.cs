using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _tripleshotPrefab;
    [SerializeField]
    private GameObject _enemy1Prefab;
    [SerializeField] 
    private GameObject _enemyContainer;
    private bool _tripleshotActive = false;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Enemy1_SpawnRoutine());
        StartCoroutine(TripleShot_SpawnRoutine());
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
            GameObject newEnemy1 = Instantiate(_enemy1Prefab, transform.position + new Vector3(randomX, 9, 0), Quaternion.identity);
            newEnemy1.transform.parent = _enemyContainer.transform;
            yield return new WaitForSecondsRealtime(5f);
        }
    }

    IEnumerator TripleShot_SpawnRoutine()
    {
        while (_tripleshotActive == false)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            GameObject newTripleShot_PowerUp = Instantiate(_tripleshotPrefab, transform.position + new Vector3(randomX, 9, 0), Quaternion.identity);
            yield return new WaitForSecondsRealtime(Random.Range(3f,7f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
