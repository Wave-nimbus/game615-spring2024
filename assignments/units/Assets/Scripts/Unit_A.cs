using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;
using UnityEngine.AI;

public class Unit_A : MonoBehaviour
{
    public GameObject gameCont; //GameControlelr reference
    public float rayLength;

    public Renderer myRend; //Change color using this.
    public Color none;
    public Color hover;
    public Color select;
    public bool amSelected;
    public GameObject visionSphere;

    public string myName;
    public int myCapacity;
    public int inventory; //Unit stats

    public GameObject currBase;
    public GameObject currGather; //Assigned base & gathering node.
    private GameObject currNode; //Where the Unit is standing currently.

    public float harvestTimer;
    public float depositTimer;

    NavMeshAgent navMA;

    public enum Activity //Current job status
    {
        Idle, Active, Gathering, Depositing
    };
    public Activity myActivity;

    // Start is called before the first frame update
    void Start()
    {
        gameCont = GameObject.Find("GameController");
        rayLength = 10;
        visionSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 smaller = new Vector3(0.3f, 0.3f, 0.3f);
        visionSphere.transform.localScale = smaller;

        navMA = GetComponent<NavMeshAgent>();

        myName = "Hello World";
        myCapacity = 10;
        inventory = 0;
        harvestTimer = 1f;
        depositTimer = 0.2f;
        

        myActivity = Activity.Idle;
        none = Color.yellow;
        hover = Color.magenta;
        select = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (myActivity != Activity.Gathering && myActivity != Activity.Depositing)
        {
            if (currBase != null && currGather != null) //If no work is assigned, be idle.
                myActivity = Activity.Active;
            else
                myActivity = Activity.Idle;
        }


        if(myActivity == Activity.Active && inventory != myCapacity) //If capacity is not full, go to the gathering point.
        {
            GoToPoint(currGather.transform.position);
        }
        else if(myActivity == Activity.Active && inventory == myCapacity) //Else, go to the base.
        {
            GoToPoint(currBase.transform.position);
        }
    }

    public void GoToPoint(Vector3 point) //Public Nav Mesh destination setter.
    {
        navMA.SetDestination(point);
    }

    private void OnMouseDown() //"Clicked on the unit."
    {
        gameCont.GetComponent<GameController>().selectUnit(this);
        gameCont.GetComponent<GameController>().hud.SetSelection(this.gameObject);
    }

    public void setBase(GameObject newBase)
    {
        if (currBase != null)
        {
            HomeBase baseObj = currBase.GetComponent<HomeBase>();
            baseObj.rend.material.color = baseObj.none;
        }
        currBase = newBase;
    }

    public void setGather(GameObject newGather)
    {
        if (currGather != null)
        {
            GatherPoint gathObj = currGather.GetComponent<GatherPoint>();
            gathObj.rend.material.color = gathObj.none;
        }
        currGather = newGather;
    }


    private void OnTriggerEnter(Collider other) //Arrived at a gathering point or base.
    {
        if (myActivity == Activity.Active)
        {
            if (other.gameObject.CompareTag("GatherNode"))
            {
                currNode = other.gameObject;
                GoToPoint(transform.position); //Stop moving.
                myActivity = Activity.Gathering;
                HarvestLoop();
                //Debug.Log("Made it to the Gather Point");
            }
            else if (other.gameObject.CompareTag("Base"))
            {
                currNode = other.gameObject;
                GoToPoint(transform.position);
                myActivity = Activity.Depositing;
                DepositLoop();
                //Debug.Log("Made it to Base");
            }

        }
    }

    private void HarvestLoop()
    {
        if (currNode != null && currGather != null && currNode.transform.position != currGather.transform.position) //Not standing in front of assigned location.
        {
            //Debug.Log("Wrong Location");
            myActivity = Activity.Active;
            return; 
        }
        currGather.GetComponent<GatherPoint>().Harvest(this);
        if(inventory < myCapacity)
            Invoke("HarvestLoop", harvestTimer);
        else
        {
            myActivity = Activity.Active;
        }
    }

    private void DepositLoop() 
    {
        if (currNode != null && currGather != null && currNode != currBase) //Bot standing in front of asigned location.
        {
            myActivity = Activity.Active;
            return;
        }
        currBase.GetComponent<HomeBase>().Deposit(this);
        if (inventory > 0)
            Invoke("DepositLoop", depositTimer);
        else
        {
            myActivity = Activity.Active;
        }
    }


    private void RayCasting() //Remnants of previous raycasting toying.
    { 
        //Debug.DrawRay(transform.position + Vector3.up * 1.75f, transform.forward * rayLength);
        transform.Rotate(0, 20 * Time.deltaTime, 0, Space.World);
        Ray myRay = new Ray(transform.position + Vector3.up * 1.75f, transform.forward * rayLength);
        RaycastHit myHit;
        if (Physics.Raycast(myRay, out myHit, rayLength))
        {
            if (LayerMask.NameToLayer("Wall") == myHit.collider.gameObject.layer )
            {
                //Debug.Log("Hit a thing!");
                myRend.material.color = Color.red;

                visionSphere.SetActive(true);
                visionSphere.transform.position = myHit.point;
            }
        }
        else
        {
            visionSphere.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        if(!amSelected)
            myRend.material.color = hover;
    }

    private void OnMouseExit()
    {
        if(!amSelected)
            myRend.material.color = none;
    }
}
