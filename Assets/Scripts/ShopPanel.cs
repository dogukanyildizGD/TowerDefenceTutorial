using UnityEngine;

public class ShopPanel : MonoBehaviour
{

    public TurrentBluePrint standartTurrent;
    public TurrentBluePrint missleLauncher;
    public TurrentBluePrint LaserBeam;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandartTurret()
    {
        Debug.Log("Standart Turret Selected");
        buildManager.SelectTurretToBuild(standartTurrent);
    }

    public void SelectMissleLauncher()
    {
        Debug.Log("Missle Turret Selected");
        buildManager.SelectTurretToBuild(missleLauncher);
    }

    public void SelectLaserBeam()
    {
        Debug.Log("Laser Beam Selected");
        buildManager.SelectTurretToBuild(LaserBeam);
    }
}
