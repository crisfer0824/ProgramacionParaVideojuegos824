using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public float spawnInterval = 5f;
    public float speedIncreaseInterval = 5f;
    public float speedIncreaseAmount = 0.5f;

    private float timer = 0f;
    private float speedTimer = 0f;
    private float currentSpeed = 2f;

    void Update()
    {
        timer += Time.deltaTime;
        speedTimer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }

        if (speedTimer >= speedIncreaseInterval)
        {
            currentSpeed += speedIncreaseAmount;
            speedTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = player.position + new Vector3(Random.Range(-8, 8), 2, 0);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyFollow script = enemy.GetComponent<EnemyFollow>();
        script.player = player;
        script.speed = currentSpeed;
    }
}