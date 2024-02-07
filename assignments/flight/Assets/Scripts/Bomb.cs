using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float destroyTime;
    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        destroyTime = 0.75f;
        plane = FindFirstObjectByType(typeof(Plane)).ConvertTo<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if(destroyTime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            Plane planeScript = plane.GetComponent<Plane>();
            planeScript.score++;
            planeScript.Boost();
            Destroy(other.gameObject);
            Destroy(gameObject);

            
        }
    }
}
