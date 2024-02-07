using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class Plane : MonoBehaviour
{
    public float forwardSpeed;
    public float vertSpeed;
    public float rotateSpeed;
    public GameObject followCam;
    public GameObject bomb;
    public float launchSpeed;

    public int score = 0;
    public float time;
    public TMP_Text scoreText;
    public TMP_Text timer;

    private Vector3 SPAWN;
    private const float DEFAULT_SPEED = 10f;
    private const float DEFAULT_V_SPEED = 5f;
    private const float SLOW_TIME = 5f;

    private float slowTimer;
    // Start is called before the first frame update
    void Start()
    {
        SPAWN = transform.position;
        forwardSpeed = DEFAULT_SPEED;
        vertSpeed = DEFAULT_V_SPEED;
        rotateSpeed = -90f;
        //followCam = MainCamera;

        score = 0;
        time = 300f;
        slowTimer = SLOW_TIME;
        launchSpeed = 1000f;
        CamFollow();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation
        float hAxis = Input.GetAxis("Horizontal"); //Get input for Left/Right
        float vAxis = Input.GetAxis("Vertical"); //Get input for Up/Down.
        Vector3 planeRotate = new Vector3(0, 0, 0);
        planeRotate.z = rotateSpeed * hAxis * Time.deltaTime;
        planeRotate.x = rotateSpeed * vAxis * Time.deltaTime;
        transform.Rotate(planeRotate, Space.Self);

        //Movement
        
        if (Input.GetKey(KeyCode.Space)) //Boost
        {
            forwardSpeed += 2 * Time.deltaTime;
        }

        Vector3 planeMove = new Vector3();
        Vector3 planeUp = new Vector3();
        Vector3 planeDown = new Vector3();

        planeMove = (transform.forward * forwardSpeed * Time.deltaTime);
        planeUp = transform.up * vertSpeed * Time.deltaTime;
        planeDown.y = -vertSpeed * Time.deltaTime;

        planeMove += planeUp + planeDown;

        //forwardSpeed = Math.Clamp(forwardSpeed, 0, 10);
        
        transform.position += planeMove;

        //Camera Follow.
        CamFollow();

        //Fire Gun.
        if(Input.GetMouseButtonDown(0))
        {
            GameObject boom = Instantiate(bomb, transform.position, Quaternion.identity);
            boom.transform.Rotate(0, 0, 0);
            Rigidbody boomPhys = boom.GetComponent<Rigidbody>();
            boomPhys.AddForce(transform.forward * launchSpeed);
        }

        //Update Score & Time Text
        scoreText.text = "SCORE: " + score;
        time-= Time.deltaTime;
        int min = (int) Math.Floor(time / 60);
        int tenSec = (int) Math.Floor((time % 60) / 10);
        int digSec = (int )Math.Floor((time % 60) % 10);
        timer.text = "TIME: " + min + ":" + tenSec + digSec;
        if (time <= 0)
        {
            timer.text = "GAME OVER";
            Destroy(gameObject);
        }

        slowTimer -= Time.deltaTime;
        if(slowTimer <= 0 ) 
        {
            forwardSpeed -= 0.5f;
            vertSpeed -= 0.25f;
            slowTimer = SLOW_TIME;
        }
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Logged");
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("Terrain Hit");
            transform.position = SPAWN;
            transform.rotation = Quaternion.identity;
        }
    }

    private void CamFollow()
    {
        Vector3 camPos = transform.position - (transform.forward * 20f);
        camPos.y += 8f;
        followCam.transform.position = camPos;

        followCam.transform.LookAt(transform);
    }

    public void Boost()
    {
        forwardSpeed = DEFAULT_SPEED;
        vertSpeed = DEFAULT_V_SPEED;
    }
}
