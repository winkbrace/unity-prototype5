using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float maxRangeX = 4;
    private float spawnPosY = -2;

    public ParticleSystem explosionParticle;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        // appear with random upwards force and spinning
        targetRb.AddForce(randomForce(), ForceMode.Impulse);
        targetRb.AddTorque(randomTorque(), randomTorque(), randomTorque(), ForceMode.Impulse);

        // appear below the screen at random x
        transform.position = randomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.gameIsRunning) {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only triggers when we hit the Sensor
        Destroy(gameObject);

        if (! gameObject.CompareTag("Bad")) {
            gameManager.GameOver();
        }
    }
    
    private float randomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 randomPosition()
    {
        return new Vector3(Random.Range(-maxRangeX, maxRangeX), spawnPosY);
    }

    private Vector3 randomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
}
