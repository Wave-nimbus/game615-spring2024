using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    private float deathTime;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        deathTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        deathTime -= Time.deltaTime;
        if (deathTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
