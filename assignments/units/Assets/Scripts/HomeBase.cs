using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxResources;
    public int currResources;
    public Renderer rend;
    public Color none;
    public Color hover;
    public Color selected;

    public List<Material> materials;
    void Start()
    {
        maxResources = 100;
        currResources = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Deposit(Unit_A gatherer)
    {
        currResources++;
        gatherer.inventory--;
        //Debug.Log("Deposited a thing!");
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
