using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Collections.Specialized;

public class PlayerController : MonoBehaviour // This script must have the same name as well as the file name in Unity
{
    public float speed = 8; //declaring speed
    private Rigidbody rb; //declaring rigid body
    public int score = 0;
    public Text scoreBoard;
    public Text winScore;
    public bool dangerous = false;  //when true will scare the ghost
    public GameObject spawnLocation;
    public GameObject ghostSpawnLocation;
    public GameObject ScoreHolder;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); //0.0 indicates a float
        rb.AddForce(movement * speed); // adds speed when controlling an object
    }

    void Update()
    {
        scoreBoard.text = "SCORE " + score;
        //winScore.text = "YOUR SCORE IS: " + score;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            score++;
            PlayerPrefs.SetInt("YOUR SCORE IS: ", score);
            if(score == 52)
            {
                SceneManager.LoadScene("Win");
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            score = score - 5;
            transform.position = spawnLocation.transform.position;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            other.transform.position = ghostSpawnLocation.transform.position;
        }

        if (other.gameObject.CompareTag("Pill"))
        {//make ourselves to be dangerouse, then call a function to make us not dangerouse for 5 sec
            other.gameObject.SetActive(false);
            dangerous = true;
        }
    }
}
