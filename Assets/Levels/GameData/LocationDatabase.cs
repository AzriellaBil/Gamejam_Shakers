using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LocationEntry
{
    public string locationName; 
    public string description;  
}

public class LocationDatabase : MonoBehaviour
{

    [SerializeField] private List<LocationEntry> locationEntries = new List<LocationEntry>();


    private Dictionary<string, string> database = new Dictionary<string, string>();

    private void Awake()
    {
        BuildDictionary();
    }

    private void BuildDictionary()
    {
        database.Clear();

        foreach (LocationEntry entry in locationEntries)
        {
            if (!database.ContainsKey(entry.locationName))
            {
                database.Add(entry.locationName, entry.description);
            }
            else
            {
                Debug.LogWarning($"Duplicate Key : '{entry.locationName}'. The duplicate was ignored.");
            }
        }
    }

    public string GetLocationDescription(string searchKey)
    {
  
        if (database.TryGetValue(searchKey, out string foundDescription))
        {
            return foundDescription;
        }

        return "Description not found."; 
    }
}