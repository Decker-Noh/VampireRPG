using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Transform[] spawnPoint;
    public SpawnData[] spawnData;
    float timer;
    int level;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.currentGameTime / 10f), spawnData.Length-1);
        
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            MonsterSpawner();
        }
    }
    void MonsterSpawner()
    {
        GameObject enemy = GameManager.Instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].transform.position;

        enemy.GetComponent<Enemy>().EnemyInit(spawnData[level]);
    }
}
[System.Serializable]
public class SpawnData
{
    public int spriteInt;
    public float spawnTime;
    public int health;
    public float speed;
}