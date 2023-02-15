using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDestructable;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public Transform _enemiesWorldParent;
    
    [SerializeField]
    public EnemyObject _enemyDef;

    [SerializeField]
    private float spawnInterval;

    DamageVisualizer _visualizer;

    private float lastSpawned;

    private int spawnCount;

    public event Action SpawnCompleted;

    public event Action<Enemy> OnEnemySpawned;

    Coroutine spawningCoroutine;

    [SerializeField]
    private bool initExisting;

    public int SpawnCount { get => spawnCount; set => spawnCount = value; }
    public float SpawnInterval { set => spawnInterval = value; }

    public void Awake()
    {
        _visualizer = FindObjectOfType<DamageVisualizer>();

        if (initExisting)
        {
           foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                InitExistingEnemies(enemy);
            }
        }
    }

    private void InitExistingEnemies(Enemy enemy)
    {
        enemy.DamageVisualizer = _visualizer;
        enemy.Initialize();

        if (enemy.TryGetComponent(out Shooting shooting))
        {
            shooting.Weapon = enemy.EnemyObject.Weapon;
            shooting.Initialize();
        }

    }

    public void StartSpawn()
    {
       spawningCoroutine  = StartCoroutine(SpawnEnemyCoorutine());
    }
    
    public IEnumerator SpawnEnemyCoorutine()
    {
        while (spawnCount > 0)
        {
            SpawnEnemy(_enemyDef.Prefab.gameObject, Vector3.zero, _enemyDef);
            lastSpawned = Time.fixedTime;

            spawnCount--;
            yield return new WaitForSeconds(spawnInterval);
            
        }

        SpawnCompleted?.Invoke();
    }

    [ContextMenu("SpawnEnemy")]
    public void SpawnEnemy(GameObject prefab, Vector3 position, EnemyObject enemyObject)
    {
        GameObject obj = ObjectCacheManager._Instance.GetObject(prefab, false);

        if(obj == null)
            obj = Instantiate(_enemyDef.Prefab.gameObject);        
        obj.transform.SetParent(_enemiesWorldParent, false);
        obj.transform.localPosition = position;
        var enemy = obj.GetComponent<Enemy>();
        enemy.EnemyObject = enemyObject;
        InitExistingEnemies(enemy);
        OnEnemySpawned?.Invoke(enemy);
        obj.SetActive(true);
    }

    [ContextMenu("Pause")]
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    [ContextMenu("Resume")]
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    [ContextMenu("play 2x")]
    public void Speed2x()
    {
        Time.timeScale = 2f;
    }
}
