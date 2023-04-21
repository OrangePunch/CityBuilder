using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class StructureManager : MonoBehaviour
{
    public StructurePrefabWeighted[] housesPrefabe, specialPrefabs, bigStructuresPrefabs;
    public PlacementManager placementManager;
    public TextMeshProUGUI peopleText;
    public GameObject errorImage;

    private float[] houseWeights, specialWeights, bigStructureWeights;
    private CollectButton collectButton;
    private float people;
    private bool isError = false;

    private void Start()
    {
        houseWeights = housesPrefabe.Select(prefabStats => prefabStats.weight).ToArray();
        specialWeights = specialPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        bigStructureWeights = bigStructuresPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        collectButton = FindObjectOfType<CollectButton>().GetComponent<CollectButton>();
    }

    private void Update()
    {
        peopleText.text = ": " + people.ToString();

        if (isError)
        {
            errorImage.gameObject.SetActive(true);
            Time.timeScale = 0;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                errorImage.gameObject.SetActive(false);
                isError = false;
            }
        }
        else
        {
            errorImage.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void PlaceHouse(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, housesPrefabe[randomIndex].prefab, CellType.Structure, buildingPrefabIndex: randomIndex);
            AudioPlayer.instance.PlayPlacementSound();
            people += 5;
        }
    }

    internal void PlaceBigStructure(Vector3Int position)
    {
        int width = 2;
        int height = 2;

        if (CheckBigStructure(position, width , height))
        {
            if (collectButton.money >= 60)
            {
                int randomIndex = GetRandomWeightedIndex(bigStructureWeights);
                placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefabs[randomIndex].prefab, CellType.BigStructure, width, height, randomIndex);
                AudioPlayer.instance.PlayPlacementSound();
                collectButton.money -= 60;
                people += 30;
            }
            else
            {
                isError = true;
            }
        }
    }

    private bool CheckBigStructure(Vector3Int position, int width, int height)
    {
        bool nearRoad = false;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                
                if (DefaultCheck(newPosition)==false)
                {
                    return false;
                }
                if (nearRoad == false)
                {
                    nearRoad = RoadCheck(newPosition);
                }
            }
        }
        return nearRoad;
    }

    public void PlaceSpecial(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            if (people >= 15)
            {
                int randomIndex = GetRandomWeightedIndex(specialWeights);
                placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].prefab, CellType.SpecialStructure, buildingPrefabIndex: randomIndex);
                AudioPlayer.instance.PlayPlacementSound();
                people -= 15;
            }
            else
            {
                isError = true;
            }
        }
    }

    private int GetRandomWeightedIndex(float[] weights)
    {
        float sum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
        }

        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            //0->weihg[0] weight[0]->weight[1]
            if(randomValue >= tempSum && randomValue < tempSum + weights[i])
            {
                return i;
            }
            tempSum += weights[i];
        }
        return 0;
    }

    private bool CheckPositionBeforePlacement(Vector3Int position)
    {
        if (DefaultCheck(position) == false)
        {
            return false;
        }

        if (RoadCheck(position) == false)
            return false;
        
        return true;
    }

    private bool RoadCheck(Vector3Int position)
    {
        if (placementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            return false;
        }
        return true;
    }

    private bool DefaultCheck(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            //Debug.Log("This position is out of bounds");
            return false;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            //Debug.Log("This position is not EMPTY");
            return false;
        }
        return true;
    }

    internal void PlaceLoadedStructure(Vector3Int position, int buildingPrefabindex, CellType buildingType)
    {
        switch (buildingType)
        {
            case CellType.Structure:
                placementManager.PlaceObjectOnTheMap(position, housesPrefabe[buildingPrefabindex].prefab, CellType.Structure, buildingPrefabIndex: buildingPrefabindex);
                break;
            case CellType.BigStructure:
                placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefabs[buildingPrefabindex].prefab, CellType.BigStructure, 2, 2, buildingPrefabindex);
                break;
            case CellType.SpecialStructure:
                placementManager.PlaceObjectOnTheMap(position, specialPrefabs[buildingPrefabindex].prefab, CellType.SpecialStructure, buildingPrefabIndex: buildingPrefabindex);
                break;
            default:
                break;
        }
    }

    public Dictionary<Vector3Int, StructureModel> GetAllStructures()
    {
        return placementManager.GetAllStructures();
    }

    public void ClearMap()
    {
        placementManager.ClearGrid();
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(0,1)]
    public float weight;
}
