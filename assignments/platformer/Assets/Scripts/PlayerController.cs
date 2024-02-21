using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float hAxis;
    private float vAxis;
    public float rotateSpeed;
    public float forwardSpeed;

    private float yVel;
    public float gravity;
    public float jumpForce;
    public CharacterController chaCon;
    public Animator anim;

    public TMP_Text starCountText;
    private int starCount;
    public TMP_Text timer;
    private float timerTime;

    public GameObject camOrigin;
    public GameObject visuals;
    public Camera mainCam;
    private Transform lookAtTarget;
    private float camDamping;
    private bool isJumping;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = 180f;
        forwardSpeed = 12f;
        yVel = 0f;
        gravity = -20f;
        jumpForce = 10f;
        chaCon = gameObject.GetComponent<CharacterController>();

        camDamping = 8f;
        mainCam.transform.Rotate(15, 0, 0);
        camOrigin.transform.position = transform.position;
        moveCam();
        timerTime = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        if (visuals.transform.position.y - transform.position.y > 0.25f)
            visuals.transform.position = transform.position;

        movement();
        camOrigin.transform.position = transform.position;
        moveCam();
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        if (vAxis != 0f || hAxis != 0f)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }

        if(starCount < 5)
            timerTime -= Time.deltaTime;
        timer.text = "" + ((int)timerTime / 60) + ":" + ((int)timerTime % 60 / 10) + ((int)timerTime % 60 % 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Collider:" + other);
        if (other.CompareTag("Platform"))
        {
            Debug.Log("Landed on Plat");
            transform.SetParent(other.transform);
        }

        //Stars & Other Collectables.
        else if (other.CompareTag("Collectable"))
        {
            Debug.Log("Shine Get!");
            starCount++;
            starCountText.text = ":0" + starCount;
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Platform"))
        {
            Debug.Log("Exited Plat");
            transform.parent = null;
        }
    }

    private void moveCam()
    {

        Vector3 currCamPos = mainCam.transform.position;
        Vector3 nextCamPos = camOrigin.transform.position;
        nextCamPos.z += -8.5f;
        nextCamPos.y += 6f;

        mainCam.transform.position += (nextCamPos - currCamPos) * Time.deltaTime * camDamping;
        //mainCam.transform.Rotate(15, 0, 0);
    }

    private void movement()
    {
        
        lookAtTarget = camOrigin.transform;
        //Walking Controls
        Vector3 camForward = mainCam.transform.forward * vAxis;
        camForward.y = 0;
        Vector3 camRight = mainCam.transform.right * hAxis;
        camRight.y = 0;
        Vector3 moveVec = camForward + camRight;
        
        //Grounded & Jump Logic
        if (!chaCon.isGrounded)
        {
            yVel += gravity * Time.deltaTime;

            if (yVel > 0 && Input.GetKeyUp(KeyCode.Space))
            {
                yVel = 0;
            }
        }
        else
        {
            yVel = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVel = jumpForce;
                anim.SetTrigger("jump");
                isJumping = true;
            }

        }
        if (chaCon.isGrounded)
            isJumping = false;
        
        moveVec *= forwardSpeed;
        lookAtTarget.position += moveVec;
        if (!isJumping && Mathf.Abs(vAxis) > 0.25 || Mathf.Abs(hAxis) > 0.25)
        { 
            visuals.transform.LookAt(lookAtTarget); 
        }   
        moveVec.y = yVel;
        chaCon.Move(moveVec * Time.deltaTime);
    }
}
