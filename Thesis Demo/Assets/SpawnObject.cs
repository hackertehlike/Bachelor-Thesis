using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System.Collections;
using HouseLayout;

public class SpawnObject : MonoBehaviour
{

    public House house;
    public Dictionary<string, string> uid_to_jid;

    // Start is called before the first frame update
    void Start()
    
    {   

        // Read json file and initialize the House object

        string jsonFilePath = @"Assets/house.json";
        string json = File.ReadAllText(jsonFilePath);
        house = JsonConvert.DeserializeObject<House>(json);

        // Get the list of furniture in the house

        List<Furniture> furnitureList = house.furniture;

        // Save uid to jid mappings

        uid_to_jid = new Dictionary<string, string>();

        foreach (Furniture f in furnitureList) {
            string uid = f.uid;
            string jid = f.jid;

            // Debug.Log("uid: " + uid + " jid: " + jid);
            if(!uid_to_jid.ContainsKey(uid)) {uid_to_jid.Add(uid, jid);}
        }

        /* foreach(var pair in uid_to_jid) {

             Debug.Log("Key: " + pair.Key + ", Value: " + pair.Value);
        }*/

        // SpawnModel("a0b67c64-15a4-4969-91a6-89e365d87d12", new Vector3(0,0,10), Quaternion.identity, Vector3.one);

        SpawnRoom(2);
       

    }

    public void SpawnModel(string filename, Vector3 position, Quaternion rotation, Vector3 scale) {

        if (position == null) position = Vector3.zero;

        GameObject go = Resources.Load(filename + "/raw_model") as GameObject;

        var clone = Instantiate(go, position, rotation);
        clone.name = filename;
        go.transform.localScale = scale;
    }


    public void SpawnRoom(int roomIndex) {

        int spawned = 0;

        Room room = house.scene.room[roomIndex];

        // Create the scene
        // Scene newScene = SceneManager.CreateScene(room.instanceid);

        // Get the list of furniture in the room 

        List<Children> furniture_room = room.children;

        // Spawn the objects


        foreach (Children c in furniture_room) {

            string uid = c.@ref;
            //Debug.Log("next uid: " + uid);
            List<double> pos = c.pos;
            List<double> rot = c.rot;
            List<double> scale = c.scale;

            // get jid from dictionary

            if (uid_to_jid.ContainsKey(uid)) {
                string jid = uid_to_jid[uid];
                //Debug.Log("jid: " + jid);
                //Debug.Log("pos: " + pos);
                try {
                SpawnModel(jid, new Vector3((float) pos[0], (float) pos[1], (float) pos[2]), new Quaternion((float) rot[0], (float) rot[1], (float) rot[2], (float) rot[3]), new Vector3((float) scale[0],(float) scale[1], (float) scale[2]));
                spawned++;
                } catch (System.Exception e) {
                    continue;
                }
            } else
            {
                //Debug.Log("uid not found");
            }
        }

        Debug.Log("Spawned: " + spawned);
    }

   
}

