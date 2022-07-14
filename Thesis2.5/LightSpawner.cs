using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class LightSpawner : MonoBehaviour
{
    public HouseManager hm;

    public ObjectSpawner os;

    House house;

    List<Lights> lightsList;

    List<Vector3> lampLocations;

    // Start is called before the first frame update
    void Start()
    {
        ReadLights();
        SpawnDayLight();
        SpawnLampLights();
    }

    // Read lights
    void ReadLights()
    {
        string house_id = hm.house_id;
        string jsonFilePath = @"Assets/" + house_id + ".json";
        string json = File.ReadAllText(jsonFilePath);
        house = JsonConvert.DeserializeObject<House>(json);

        lightsList = house.lights;
    }

    // Spawn all the lights in the house
    void SpawnDayLight()
    {
        foreach (Lights l in lightsList)
        {
            if (l.src_position == null) continue;

            // Make a game object
            GameObject lightGameObject = new GameObject(l.uid);

            // Add the light component
            Light lightComp = lightGameObject.AddComponent<Light>();

            // Set color and position
            lightComp.color = Color.white;

            lightComp.intensity = 0.2f;

            // Set the position (or any transform property)
            lightGameObject.transform.position =
                new Vector3((float) l.src_position[0],
                    (float) l.src_position[1],
                    (float) l.src_position[2]);
        }
    }

    void SpawnLampLights()
    {
        lampLocations = os.GetLampLocations();

        Debug.Log("Lamp locations: " + lampLocations.Count);

        foreach (Vector3 v in lampLocations)
        {
            Debug.Log (v);

            // Make a game object
            GameObject lightGameObject = new GameObject("lamplight");

            // Add the light component
            Light lightComp = lightGameObject.AddComponent<Light>();

            // Set color and position
            lightComp.color = Color.white;

            lightComp.intensity = 0.2f;

            // Set the position (or any transform property)
            lightGameObject.transform.position = v;
        }
    }
}
