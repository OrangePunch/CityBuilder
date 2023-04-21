using UnityEngine;

public class CommercialBuilding : MonoBehaviour
{
    public BuildingType buildingType;
    public GameObject _coin;
    public int coinsPerCollection = 10;
    public int coinsAvailable;

    private void Start()
    {
        coinsAvailable = Random.Range(10, coinsPerCollection);
    }

    public int CollectCoins()
    {
        int coinsCollected = coinsAvailable;
        coinsAvailable = 0;

        Debug.Log("Collected " + coinsCollected + " coins from " + buildingType.ToString() + ".");

        return coinsCollected;
    }
}
