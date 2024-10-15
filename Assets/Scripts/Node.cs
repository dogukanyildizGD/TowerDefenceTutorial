using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor;
    public Vector3 positionOffset;
    public Color notHasMoneyColor;

    private Renderer rend;
    private Color startColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurrentBluePrint turrentBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
                       
    }

    private void OnMouseDown()
    {
        // EventSystem.current.IsPointerOverGameObject() menu'den objeye tıkladığımızda
        // arkaplana tıklanma yapılmasını engelliyor.
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());

    }

    void BuildTurret(TurrentBluePrint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not Enough Money!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position + positionOffset, Quaternion.identity);
        turret = _turret;

        turrentBluePrint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.effectBuild, transform.position + positionOffset, Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Current Money : " + PlayerStats.Money);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turrentBluePrint.upgradeCost)
        {
            Debug.Log("Not Enough Money to Upgrade !");
            return;
        }

        PlayerStats.Money -= turrentBluePrint.upgradeCost;

        // Eski turret'i yok et.
        Destroy(turret);

        // Yeni turret'a güncelle.
        GameObject _turret = (GameObject)Instantiate(turrentBluePrint.upgradePrefab, transform.position + positionOffset, Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.effectBuild, transform.position + positionOffset, Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret Upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turrentBluePrint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.effectSell, transform.position + positionOffset, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turrentBluePrint = null;

        isUpgraded = false;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        // Eğer döndürülmez ise hoverColor obje ile birlikte
        // node'un üzerinde tekrar oluşur.
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notHasMoneyColor;
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
