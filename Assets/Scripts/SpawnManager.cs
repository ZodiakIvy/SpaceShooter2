using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _Enemy1Prefab;
    [SerializeField] 
    private GameObject _EnemyContainer;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-8.5f, 7.6f);
            GameObject newEnemy1 = Instantiate(_Enemy1Prefab, transform.position + new Vector3(randomX, 9, 0), Quaternion.identity);
            newEnemy1.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSecondsRealtime(5f);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
