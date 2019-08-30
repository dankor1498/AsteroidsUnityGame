using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEmiterScript : MonoBehaviour
{
    public float minDelay, maxDelay, time, timeProtection, timeFire;

    public GameObject Fire, Protection;
    public GameObject Asteroid, AsteroidS, AsteroidXXL;
    public GameObject Enemy0, Enemy1, Enemy2;

    enum asteroids { asteroid, asteroidS, asteroidXXL };
    enum Enemies { enemy0, enemy1, enemy2 };

    private float nextSpawn, nextSpawnEnemy, nextSpawnProtection, nextSpawnFire;
    private GameController controller;

    void Start()
    {
        nextSpawnProtection = 6;
        controller = GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.isGameStarted())
        {
            return;
        }

        if (Time.time > nextSpawnProtection)
        {
            float YPos = transform.position.y;
            float ZPos = transform.position.z;
            float XPos = Random.Range(-19 / 2, 19 / 2);

            Vector3 randomPosition = new Vector3(XPos, YPos, ZPos);
            Instantiate(Protection, randomPosition, Quaternion.identity);
            nextSpawnProtection = Time.time + timeProtection;
        }

        if (Time.time > nextSpawnFire)
        {
            float YPos = transform.position.y;
            float ZPos = transform.position.z;
            float XPos = Random.Range(-19 / 2, 19 / 2);

            Vector3 randomPosition = new Vector3(XPos, YPos, ZPos);
            Instantiate(Fire, randomPosition, Quaternion.identity);
            nextSpawnFire = Time.time + timeFire;
        }

        if (Time.time > nextSpawnEnemy)
        {
            float YPos = transform.position.y;
            float ZPos = transform.position.z;

            float XPos = Random.Range(-19 / 2, 19 / 2);

            Vector3 randomPosition = new Vector3(XPos, YPos, ZPos);

            Enemies a = (Enemies)Random.Range(0, 3);
            switch (a)
            {
                case Enemies.enemy0: Instantiate(Enemy0, randomPosition, Quaternion.identity); break;
                case Enemies.enemy1: Instantiate(Enemy1, randomPosition, Quaternion.identity); break;
                case Enemies.enemy2: Instantiate(Enemy2, randomPosition, Quaternion.identity); break;
            }

            nextSpawnEnemy = Time.time + time;
        }

        if (Time.time > nextSpawn)
        {
            float YPos = transform.position.y;
            float ZPos = transform.position.z;

            float XPos = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

            Vector3 randomPosition = new Vector3(XPos, YPos, ZPos);

            asteroids a = (asteroids)Random.Range(0, 3);
            switch (a)
            {
                case asteroids.asteroid: Instantiate(Asteroid, randomPosition, Quaternion.identity); break;
                case asteroids.asteroidS: Instantiate(AsteroidS, randomPosition, Quaternion.identity); break;
                case asteroids.asteroidXXL: Instantiate(AsteroidXXL, randomPosition, Quaternion.identity); break;
            }

            nextSpawn = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
