using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than BuildManager in scene!");
        }
        instance = this;
    }

    public GameObject effectBuild;
    public GameObject effectSell;


    private TurrentBluePrint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money > turretToBuild.cost; } }
        
    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);       
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurrentBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurrentBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
