using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidXXLScript : MonoBehaviour
{
    public float rotationSpeed;

    public GameObject asteroidExplosion, playerExplosion;

    public float minSpeed;
    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody Asteroid = GetComponent<Rigidbody>();
        Asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        Asteroid.velocity = Vector3.back * Random.Range(minSpeed, maxSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GameBoundary" || other.gameObject.tag == "Enemy"
            || other.gameObject.tag == "Asteroid" || other.gameObject.tag == "Fire"
            || other.gameObject.tag == "Protection")
        {
            return;
        }

        if (PlayerScript.ifProtection == true)
        {
            Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);

            GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>()
                .increaseScore(1);

            return;
        }

        if (other.gameObject.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
        }

        Instantiate(asteroidExplosion, transform.position, Quaternion.identity);

        GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>()
                .increaseScore(1);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
