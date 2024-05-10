
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private PatrollingWay _patrollingWay;

    private void Start()
    {
        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        enemy.gameObject.name += Random.value;
        enemy.ReplacePatrollingWay(_patrollingWay);
    }
}
