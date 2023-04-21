using UnityEngine;
using UnityEngine.UI;

public class MoneyCollector : MonoBehaviour
{
    public Text moneyText; // Text to display the player's money
    public Button collectButton; // Button to trigger money collection
    public float collectionInterval; // Time in seconds between money collections
    public GameObject _coin;

    private float lastCollectionTime = 0f; // Time when money was last collected

    private float money = 0f; // Current amount of money the player has

    void Start()
    {
        // Set up the collect button to call the CollectMoney function when clicked
        collectButton.onClick.AddListener(CollectMoney);
    }

    void Update()
    {
        // Check if it's time to collect money again
        if (Time.time - lastCollectionTime <= collectionInterval)
        {
            _coin.gameObject.SetActive(true);
            CollectMoney();
        }
    }

    void CollectMoney()
    {
        // Find all the commercial buildings in the game
        GameObject[] commercialBuildings = GameObject.FindGameObjectsWithTag("Commercial");

        // Collect money from each commercial building
        foreach (GameObject building in commercialBuildings)
        {
            money += 10;
        }

        _coin.gameObject.SetActive(false);

        // Update the money text to display the new amount of money
        moneyText.text = money.ToString();

        // Record the time of the last money collection
        lastCollectionTime = Time.time;
    }
}
