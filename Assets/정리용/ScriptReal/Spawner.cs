using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    float timer;
    int level;
    private void Start()
    {
        Spawn();
    }
    private void Update()
    {
        
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 16f),spawnData.Length-1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();

        }
    }

    void Spawn()
    {
        for(int i=0; i<spawnData[level].count; i++)
        {
            GameObject enemy = GameManager.instance.pool.Get(level + 6);
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnData[level]);
        } 
        
    }

    public void Boss()
    {
        GameObject enemy = GameManager.instance.pool.Get(33);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[spawnData.Length-1]);
    }
    
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int health;
    public float speed;
    public int count = 1;
}
