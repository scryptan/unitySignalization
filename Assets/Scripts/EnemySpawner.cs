using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    
    private float _spawnRateInSeconds = 2;
    private Transform[] _spawnPoints;
    
    private void Start()
    {
        _spawnPoints = new Transform[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
            _spawnPoints[i] = transform.GetChild(i);

        StartCoroutine(SpawnEnemyInSeconds(_spawnRateInSeconds));
    }

    private IEnumerator SpawnEnemyInSeconds(float spawnRateInSeconds)
    {
        while (true)
        {
            var randomPoint = Random.Range(0, _spawnPoints.Length);
            Instantiate(_enemyPrefab, _spawnPoints[randomPoint].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRateInSeconds);
        }
    }
}
