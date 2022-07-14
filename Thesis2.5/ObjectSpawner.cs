using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public HouseManager hm;

    House house;

    List<Furniture> furnitureList;

    Dictionary<string, string> uid_to_jid;

    Dictionary<string, string> jid_to_uid;

    Dictionary<string, string> instanceid_to_uid;

    Dictionary<string, string> uid_to_title;

    List<Vector3> lampLocations;

    void Awake()
    {
        // Initialize the necessary data structures
        uid_to_jid = new Dictionary<string, string>();
        jid_to_uid = new Dictionary<string, string>();
        instanceid_to_uid = new Dictionary<string, string>();
        uid_to_title = new Dictionary<string, string>();
        lampLocations = new List<Vector3>();

        // Read the json file and load the furniture list
        ReadAndLoad();

        // Save uid to jid mappings
        SaveMappings();

        // Spawn all the furniture in the house
        SpawnHouse();
    }

    void ReadAndLoad()
    {
        string house_id = hm.house_id;

        // Read json file and initialize the House object
        string jsonFilePath = @"Assets/" + house_id + ".json";
        string json = File.ReadAllText(jsonFilePath);
        house = JsonConvert.DeserializeObject<House>(json);

        // Get the list of furniture in the house
        furnitureList = house.furniture;
    }

    void SaveMappings()
    {
        foreach (Furniture f in furnitureList)
        {
            string uid = f.uid;
            string jid = f.jid;
            string title = f.title;

            if (!uid_to_jid.ContainsKey(uid)) uid_to_jid.Add(uid, jid);
            if (!jid_to_uid.ContainsKey(jid)) jid_to_uid.Add(jid, uid);
            if (!uid_to_title.ContainsKey(uid)) uid_to_title.Add(uid, title);
        }
    }

    void SpawnHouse()
    {
        // Spawn all rooms in the house
        for (int i = 0; i < house.scene.room.Count; i++)
        {
            SpawnRoom (i);
        }
    }

    void SpawnRoom(int room_id)
    {
        if (house.scene.room[room_id].empty == 1) return;

        // Get the list of furniture in the room
        List<Children> furniture_room = house.scene.room[room_id].children;

        foreach (Children c in furniture_room)
        {
            string uid = c.@ref;
            string instanceid = c.instanceid;
            List<double> pos = c.pos;
            List<double> rot = c.rot;
            List<double> scale = c.scale;

            // get jid from dictionary
            if (uid_to_jid.ContainsKey(uid))
            {
                string jid = uid_to_jid[uid];

                try
                {
                    // If the furniture is a lamp, save its location
                    if (uid_to_title[uid].Contains("lighting"))
                    {
                        Debug.Log("I've found a lamp!");
                        lampLocations
                            .Add(new Vector3((float) pos[0],
                                (float) pos[1],
                                (float) pos[2]));
                    }

                    // Spawn the furniture
                    instanceid_to_uid.Add (instanceid, uid);
                    SpawnModel(instanceid,
                    new Vector3((float) pos[0], (float) pos[1], (float) pos[2]),
                    new Quaternion((float) rot[0],
                        (float) rot[1],
                        (float) rot[2],
                        (float) rot[3]),
                    new Vector3((float) scale[0],
                        (float) scale[1],
                        (float) scale[2]));
                }
                catch (System.Exception e)
                {
                    //Debug.Log(e);
                }
            }
        }
    }

    void SpawnModel(
        string instanceid,
        Vector3 position,
        Quaternion rotation,
        Vector3 scale
    )
    {
        if (position == null) position = Vector3.zero;

        string uid = instanceid_to_uid[instanceid];
        string jid = uid_to_jid[uid];

        GameObject go = Resources.Load(jid + "/raw_model") as GameObject;

        var clone = Instantiate(go, position, rotation);
        clone.transform.localScale = scale;
        clone.name = instanceid;

        // Remove shadows from the model
        var shadow = clone.transform.Find("shadow").gameObject;
        if (shadow == null)
        {
            return;
        }

        DestroyImmediate(shadow, true);
    }

    public List<Furniture> GetFurnitureList()
    {
        return furnitureList;
    }

    public List<Vector3> GetLampLocations()
    {
        return lampLocations;
    }
}
