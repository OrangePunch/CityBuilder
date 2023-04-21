using UnityEngine;
using TMPro;

public class CollectButton : MonoBehaviour
{
    public BuildingType buildingType;
    public float collectionInterval;
    public TextMeshProUGUI _moneyText;
    public float money;

    private float lastCollectionTime;

    private void Start()
    {
        lastCollectionTime = Time.time - collectionInterval;
    }

    private void Update()
    {
        if (Time.time - lastCollectionTime >= collectionInterval)
        {
            CommercialBuilding[] buildings = FindObjectsOfType<CommercialBuilding>();

            foreach (CommercialBuilding building in buildings)
            {
                if (building.buildingType == buildingType)
                {
                    building._coin.gameObject.SetActive(true);
                }
            }
        }

        _moneyText.text = ": " + money.ToString();
    }

    public void CollectCoins()
    {
        if (Time.time - lastCollectionTime > collectionInterval)
        {
            int coinsCollected = 0;

            CommercialBuilding[] buildings = FindObjectsOfType<CommercialBuilding>();

            foreach (CommercialBuilding building in buildings)
            {
                if (building.buildingType == buildingType)
                {
                    coinsCollected += building.CollectCoins();
                    building._coin.gameObject.SetActive(false);
                    money += 10;
                }
            }

            Debug.Log("Collected " + coinsCollected + " coins from " + buildingType.ToString() + " buildings.");

            lastCollectionTime = Time.time;
        }
    }
}