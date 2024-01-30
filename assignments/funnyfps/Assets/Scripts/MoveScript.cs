using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float speed;
    public GameObject confetti;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.transform.position += (Time.deltaTime * gameObject.transform.forward * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //gameObject.GetComponent<Material>().color = Color.blue;
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color = Color.blue;
        for (int i = 0; i < 50; i++)
        {
            GameObject confet = Instantiate(confetti, gameObject.transform.position, Quaternion.identity);
            confet.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            confet.GetComponent<Rigidbody>().AddForce(confet.transform.forward * 5);
        }
        Destroy(gameObject);
    }
}
