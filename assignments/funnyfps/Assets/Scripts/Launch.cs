using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public GameObject bullet;
    public float fireSpeed;
    // Start is called before the first frame update
    void Start()
    {
        fireSpeed = 3000f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 launchSpot = gameObject.transform.position + (Vector3.up * 1.5f) + transform.forward;
            GameObject bul = Instantiate(bullet, launchSpot , Quaternion.identity);
            bul.GetComponent<Rigidbody>().AddForce(fireSpeed * transform.forward);
        }
    }
}
