using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GameController: MonoBehaviour
{

    public Camera mainCam;
    public Unit_A currUnit;
    public GameObject newUnit;
    //UI elements.
    public HUD hud;
    public GameObject charPanel;
    public TMP_Text unitName;

    public List<HomeBase> allBases;
    private int currResourceTotal;
    private int nextMilestone;
    private int sequence;

    public GameObject mountain;
    public GameObject baseCamp;


    // Start is called before the first frame update
    void Start()
    {
        InitSequence();
        mountain.SetActive(false);
        baseCamp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) //On left click
        {
            Ray myRay = mainCam.ScreenPointToRay(Input.mousePosition); //Raycast into the scene.
            RaycastHit myHit;
            if (currUnit != null && Physics.Raycast(myRay, out myHit)) //Unit assignments.
            {
                GameObject clicked = myHit.collider.gameObject;
                if (LayerMask.NameToLayer("Ground") == clicked.layer) //Ground navigation.
                {
                    //Ignore ground navigation. Creates bugs.
                    //Debug.Log("Hit the ground at: " + myHit.point);
                    //currUnit.GoToPoint(myHit.point);
                }
                else if (clicked.CompareTag("GatherNode")) //Assign gather node.
                {
                    currUnit.setGather(clicked);

                }
                else if (clicked.CompareTag("Base")) //Assign base node
                {
                    currUnit.setBase(clicked);
                }
            }
            else if (Physics.Raycast(myRay, out myHit))
            {
                GameObject clicked = myHit.collider.gameObject;
                if (clicked.CompareTag("GatherNode"))
                {
                    hud.SetSelection(clicked);
                }
            }
            else
            {
                if (currUnit != null)
                    unSelect();
                else
                    hud.SetSelection(null);
            }
        }
        if (currUnit != null)
        {
            showSelection();
        }

        int sum = 0;
        for (int i = 0; i < allBases.Count; i++)
        {
            sum += allBases[i].currResources;
        }
        currResourceTotal = sum;
        hud.updateProgress(currResourceTotal);
        if (currResourceTotal >= nextMilestone)
            AdvanceSequence();
    }

    public void selectUnit(Unit_A selection) //Select unit; update UI.
    {
        if (currUnit != null)
            unSelect();
        currUnit = selection;
        //unitName.text = currUnit.myName;
        //unitCap.text = "Capacity: " + currUnit.myCapacity;
        currUnit.amSelected = true;
    }

    private void showSelection()
    {
        currUnit.myRend.material.color = currUnit.select;
        if (currUnit.currBase != null)
        {
            HomeBase baseObj = currUnit.currBase.GetComponent<HomeBase>();
            for (int i = 0; i < baseObj.rend.materials.Length; i++)
            {
                baseObj.rend.materials[i].color = baseObj.selected;
            }
        }
        if (currUnit.currGather != null)
        {
            GatherPoint gathObj = currUnit.currGather.GetComponent<GatherPoint>();
            for (int i = 0; i < gathObj.rend.materials.Length; i++)
            {
                gathObj.rend.materials[i].color = gathObj.selected;
            }
        }
        //charPanel.SetActive(true);
    }

    private void unSelect()
    {
       currUnit.amSelected = false;
       currUnit.myRend.material.color = currUnit.none;
        if (currUnit.currBase != null)
        {
            HomeBase baseObj = currUnit.currBase.GetComponent<HomeBase>();
            baseObj.rend.material.color = baseObj.none;
        }
        if (currUnit.currGather != null)
        {
            GatherPoint gathObj = currUnit.currGather.GetComponent<GatherPoint>();
            gathObj.rend.material.color = gathObj.none;
        }

        //charPanel.SetActive(false);
        hud.SetSelection(null);
        currUnit = null;
    }



    //Sequence.
    private void InitSequence()
    {
        currResourceTotal = 0;
        nextMilestone = 10;
        sequence = 0;
        hud.initGoal(currResourceTotal, nextMilestone, "Second Unit");
    }

    private void AdvanceSequence()
    {
        switch (sequence)
        {
            case 0: //New unit.
                nextMilestone = 20;
                sequence++;
                Vector3 spawnPoint = new Vector3(7, 0, 0);
                Instantiate(newUnit, allBases[0].transform.position + spawnPoint, Quaternion.identity);
                //Debug.Log("Before UI Update");
                hud.updateGoal(nextMilestone, "Mountain Node");
                //Debug.Log("After UI Update");
                break;
            case 1: //Mountain appears
                nextMilestone = 40;
                sequence++;
                mountain.SetActive(true);
                hud.updateGoal(nextMilestone, "Second Base");
                break;
            case 2: //2nd base appears.
                nextMilestone = 100;
                sequence++;
                baseCamp.SetActive(true);
                hud.updateGoal(nextMilestone, "End of Game");
                break;
            default:
                break;
        }
    }



}
