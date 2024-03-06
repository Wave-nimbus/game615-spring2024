using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    public GameObject currSelection;
    public GameObject unitInfoPanel;

    public List<GameObject> unitHUD;

    private TMP_Text nameHUD;
    private Meter meterOne;
    private Meter meterTwo;

    public TMP_Text goalText;
    public Meter goalMeter;
    // Start is called before the first frame update
    void Start()
    {
        unitInfoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currSelection != null && currSelection.CompareTag("GatherNode"))
        {
            GatherNodeInfo();
        }
        else if (currSelection != null && currSelection.CompareTag("Unit"))
        {
            UnitInfo();
        }
    }

    public void SetSelection(GameObject selection)
    {
        //Debug.Log("Selected: " + selection);
        if (selection == null)
        {
            currSelection = null;
            unitInfoPanel.SetActive(false);
            return;
        }
        currSelection = selection;
        if (selection.CompareTag("Unit"))
            UnitInfo();
        else if (selection.CompareTag("GatherNode"))
            GatherNodeInfo();

        unitInfoPanel.SetActive(true);
        //Debug.Log("exiting Selection function");
    }


    private void UnitInfo()
    {
        Unit_A unit = currSelection.GetComponent<Unit_A>();
        nameHUD = unitHUD[0].ConvertTo<TMP_Text>();
        meterOne = unitHUD[1].ConvertTo<Meter>();
        unitHUD[2].SetActive(false);

        nameHUD.text = "Name: " + unit.myName;
        meterOne.SetBothVals(unit.inventory, unit.myCapacity);
        meterOne.SetLabelVals("Inventory");
    }

    private void GatherNodeInfo()
    {
        GatherPoint gathP = currSelection.GetComponent<GatherPoint>();
        nameHUD = unitHUD[0].ConvertTo<TMP_Text>();
        meterOne = unitHUD[1].ConvertTo<Meter>();
        meterTwo = unitHUD[2].ConvertTo<Meter>();

        nameHUD.text = "Resource: " + gathP.myResource;
        meterOne.SetBothVals(gathP.currResources, gathP.maxResources);
        meterOne.SetLabelVals("Resources");
        meterTwo.SetBothVals(gathP.refreshReset - gathP.refreshTimer, gathP.refreshReset);
        meterTwo.SetLabelOnly("Refresh");

        unitHUD[2].SetActive(true);
    }


    public void updateGoal(int newMax, string newBonus)
    {
        goalMeter.SetMaxVal(newMax);
        goalMeter.SetLabelVals("Progress");
        goalText.text = "Next Bonus: " + newBonus;
    }

    public void updateProgress(int currResoruces)
    {
        goalMeter.SetCurrValue(currResoruces);
        goalMeter.SetLabelVals("Progress");
    }

    public void initGoal(int start, int max, string bonus)
    {
        goalMeter.SetBothVals(start, max);
        goalMeter.SetLabelVals("Progress");
        goalText.text = "Next Bonus: " + bonus;
    }
}
