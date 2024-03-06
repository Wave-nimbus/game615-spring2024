using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{
    // Start is called before the first frame update

    private float maxVal;
    private float currVal;
    private string label;

    public Color bgColor;
    public Color fgColor;
    public Image bgImage;
    public Image fgImage;

    public TMP_Text myLabel;
    void Start()
    {
        bgImage.color = bgColor;
        fgImage.color = fgColor;
        SetBothVals(50, 100);
    }
    public void SetMaxVal(float newVal)
    {
        
        maxVal = Mathf.Max(0, newVal);
        currVal = Mathf.Clamp(currVal, 0, maxVal);
        fgImage.fillAmount = Mathf.Clamp01(currVal / maxVal);
    }
    
    public void SetCurrValue(float newVal)
    { 
        currVal = Mathf.Clamp(newVal, 0, maxVal);
        fgImage.fillAmount = Mathf.Clamp01(currVal / maxVal);
    }

    public void SetBothVals(float newCurr, float newMax)
    {
        maxVal = Mathf.Max(newMax, 0);
        currVal = Mathf.Clamp(newCurr, 0, maxVal);
        fgImage.fillAmount = Mathf.Clamp01(currVal / maxVal);
    }

    public void SetLabelVals(string newLabel)
    {
        if (label == "") //Empty string = turn off the label.
        {
            myLabel.text = "";
            return;
        }
        label = newLabel;
        myLabel.text = "" + label + " " + currVal + "/" + maxVal;
        //Ex: Inventory 50/50
    }

    public void SetLabelOnly(string newLabel)
    {
        if (label == "") //Empty string = turn off the label.
        {
            myLabel.text = "";
            return;
        }
        label = newLabel;
        myLabel.text = label;
    }
}
