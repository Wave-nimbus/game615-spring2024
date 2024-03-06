using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public int maxResources;
    public int currResources;
    public float refreshTimer;
    public float refreshReset;
    public int refreshNumber;
    public string myResource;
    public int resourceValue;

    public Renderer rend;
    public Color none;
    public Color hover;
    public Color selected;
    void Start()
    {
        rend.material.color = none;

        maxResources = 100;
        currResources = 25;
        refreshNumber = 2;
        refreshReset = 10f;
        refreshTimer = refreshReset;
    }

    // Update is called once per frame
    void Update()
    {
        refreshTimer -= Time.deltaTime;
        if (refreshTimer <= 0)
        {
            refreshTimer = refreshReset;
            currResources = Mathf.Min(currResources + refreshNumber, maxResources); //Replenish resources, maxed out as appropriate.
        }
    }

    public void Harvest(Unit_A gatherer)
    {
        if (currResources > 0)
        {
            currResources -= resourceValue;
            gatherer.inventory += resourceValue;
            //Debug.Log("Gathered a thing!");
        }
    }

    private void OnMouseOver()
    {
        for (int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].color = hover;
        }
    }

    private void OnMouseExit()
    {
        for (int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].color = none;
        }
    }
}
