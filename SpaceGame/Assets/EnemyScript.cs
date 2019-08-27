using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed, Size;

    public float xMin, xMax, Tilt;

    public GameObject shot;

    public GameObject playerExplosion;

    public Transform gunPosition;
    private GameObject player; 

    public float shotDelay, speedX;

    private float nextShot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody Enemy = GetComponent<Rigidbody>();
        Enemy.velocity = new Vector3(player.transform.position.x / speedX, 0, -1) * speed;

        float newX = Mathf.Clamp(Enemy.position.x, xMin, xMax);
        float newZ = Enemy.position.z;
        float newY = Enemy.position.y;

        Enemy.position = new Vector3(newX, newY, newZ);

        Enemy.rotation = Quaternion.Euler(-Enemy.velocity.z * Tilt, 180, Enemy.velocity.x * Tilt);

        float sizeP = Enemy.position.x + Size;
        float sizeM = Enemy.position.x - Size;
        if (player.transform.position.x <= sizeP && player.transform.position.x >= sizeM)
        {
            Enemy.velocity = Vector3.back * speed;
            Enemy.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;
            GameObject sh = Instantiate(shot, gunPosition.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shot" || other.gameObject.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
            if (PlayerScript.ifProtection == true)
            {
                Destroy(gameObject);
                GameObject
                    .FindGameObjectWithTag("GameController")
                    .GetComponent<GameController>()
                    .increaseEnemy(1);
                return;
            }
            GameObject
                    .FindGameObjectWithTag("GameController")
                    .GetComponent<GameController>()
                    .increaseEnemy(1);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
