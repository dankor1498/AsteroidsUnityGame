using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;

    public float xMin, xMax, zMin, zMax, Tilt;

    public GameObject shot, shotL, shotR, playerExplosion, Shield;

    public Transform gunPosition, gunPositionL, gunPositionR;

    public float shotDelay;

    private float nextShot, nextShot4;

    private bool ifFire = false;
    private float timeFire;

    static internal bool ifProtection = false;
    private float timeProtection;

    private GameController controller;

    void Start()
    {
        GameObject.Find("Shield").SetActive(false);

        controller = GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();
    }

    void Update()
    {
        if (!controller.isGameStarted())
        {
            return;
        }

        float shotDelay4 = shotDelay / 4;
        if (Input.GetButton("Fire1") && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;
            Instantiate(shot, gunPosition.position, Quaternion.identity);
        }

        if (Time.time - timeFire < 5 && ifFire == true)
        {
            if (Input.GetButton("Fire1") && Time.time > nextShot4)
            {
                nextShot4 = Time.time + shotDelay4;
                Instantiate(shotL, gunPositionL.position, Quaternion.identity).transform.localScale /= 4;
                Instantiate(shotR, gunPositionR.position, Quaternion.identity).transform.localScale /= 4;
            }

            if (Input.GetButton("Fire2") && Time.time > nextShot4)
            {
                nextShot4 = Time.time + shotDelay4;
                GameObject shot_L = Instantiate(shotL, gunPositionL.position, Quaternion.identity);
                shot_L.transform.localScale /= 4;
                shot_L.transform.eulerAngles = new Vector3(0, -45, 0);

                GameObject shot_R = Instantiate(shotR, gunPositionR.position, Quaternion.identity);
                shot_R.transform.localScale /= 4;
                shot_R.transform.eulerAngles = new Vector3(0, 45, 0);
            }
        }

        if (Time.time - timeProtection > 8 && ifProtection == true)
        {
            Shield.SetActive(false);
            ifProtection = false;
        }

        if (Input.GetButton("Fire2") && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;
            Instantiate(shot, gunPosition.position, Quaternion.identity);
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Rigidbody Player = GetComponent<Rigidbody>();

        Player.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        float newX = Mathf.Clamp(Player.position.x, xMin, xMax);
        float newZ = Mathf.Clamp(Player.position.z, zMin, zMax);
        float newY = Player.position.y;

        Player.position = new Vector3(newX, newY, newZ);
        Player.rotation = Quaternion.Euler(Player.velocity.z * Tilt, 0, -Player.velocity.x * Tilt);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ShotEnemy")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            if (ifProtection == true)
            {
                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == "Fire")
        {
            Destroy(other.gameObject);
            ifFire = true;
            timeFire = Time.time;
        }

        if (other.gameObject.tag == "Protection")
        {
            Destroy(other.gameObject);
            ifProtection = true;
            timeProtection = Time.time;
            Shield.SetActive(true);
        }
    }
}