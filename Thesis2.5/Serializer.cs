using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HouseLayout;
using UnityMesh = UnityEngine.Mesh;
using UnityMaterial = UnityEngine.Material;
using HouseMesh = HouseLayout.Mesh;
using HouseMaterial = HouseLayout.Material;

public class Serializer : MonoBehaviour
{
    List<Furniture> furnitureList;
    List<Room> roomList;
    List<string> materialList;
    List<Mesh> meshList;

    public ObjectSpawner objs;
    public MeshSpawner ms;
    public SerializeManager sm;
    int numRooms;


    void Start()
    {
        furnitureList = new List<Furniture>();
        roomList = new List<Room>();
        numRooms = objs.house.scene.rooms.Count;
        for(int i = 0 ; i < numRooms ; i++)
        {
            Room room = new Room();
            roomList.Add(room);
            room.children = new List<Children>();
        }
    }

    private void OnApplicationQuit() {
        if(sm.serialize) {
            SerializeHouse();
        }
    }

    House SerializeHouse() {

        MakeFurnitureList();
        MakeRooms();
        MakeMaterial();
        MakeMeshList();
        MakeScene();
        MakeMaterialList();

        House house = new House();
        house.scene = new Scene();
        house.scene.rooms = roomList;
        house.scene.furniture = furnitureList;
        house.scene.material = materialList;
        return house;
    }

    void MakeFurnitureList() {
        

        object[] obj = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (object o in obj)
          {
            GameObject g = (GameObject) o;
            Furniture f = new Furniture();
            
            string instanceid = g.name;
            string uid = objs.instanceid_to_uid[instanceid];
            string jid = objs.uid_to_jid[uid];
            string title = objs.uid_to_title[uid];

            f.uid = uid;
            f.jid = jid;
            f.title = title;
            furnitureList.Add(f);
          }
    }

    void MakeRooms() {

        object[] obj = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (object o in obj)
          {
            GameObject g = (GameObject) o;
            string instanceid = g.name;
            int roomid = objs.instanceid_to_roomid[instanceid];

            Children c = new Children();
            c.instanceid = instanceid;
            c.scale = new List<double>();
            c.rot = new List<double>();
            c.pos = new List<double>();

            c.scale.Add(g.transform.localScale.x);
            c.scale.Add(g.transform.localScale.y);
            c.scale.Add(g.transform.localScale.z);

            Quaternion q = g.transform.rotation;
            c.rot.Add(q.x);
            c.rot.Add(q.y);
            c.rot.Add(q.z);
            c.rot.Add(q.w);

            c.pos.Add(g.transform.position.x);
            c.pos.Add(g.transform.position.y);
            c.pos.Add(g.transform.position.z);

            roomList[roomid].children.Add(c);
          }

        for(int i = 0 ; i < numRooms ; i++)
        {
            roomList[i].empty = objs.house.scene.rooms[i].empty;
        }
    }

    void MakeScene() {
        Scene scene = new Scene();
        scene.room = roomList;
    }
    
    void MakeMaterialList() {
        
        materialList = objs.house.materialList;
    }

    void MakeMeshList() {
        
        object[] obj = GameObject.FindGameObjectsWithTag("Mesh");
        foreach(object o in obj)
        {
            GameObject g = (GameObject) o;

            string uid = g.name;

            HouseMesh houseMesh = new HouseMesh();
            houseMesh.uid = g.name;
            houseMesh.type = ms.meshuid_to_type[uid];
            houseMesh.material = ms.meshuid_to_materialuid[uid];

            UnityMesh unityMesh = g.GetComponent<MeshFilter>().mesh;

            houseMesh.xyz = new List<double>();
            houseMesh.normal = new List<double>();
            houseMesh.uv = new List<double>();
            houseMesh.faces = new List<int>();

            for(int i = 0 ; i < 3 ; i++)
            {
                houseMesh.xyz.Add(unityMesh.vertices[0]);
                houseMesh.xyz.Add(unityMesh.vertices[1]);
                houseMesh.xyz.Add(unityMesh.vertices[2]);
            }

            for(int i = 0 ; i < 3 ; i++)
            {
                houseMesh.normal.Add(unityMesh.normals[0]);
                houseMesh.normal.Add(unityMesh.normals[1]);
                houseMesh.normal.Add(unityMesh.normals[2]);
            }

            for(int i = 0 ; i < 2 ; i++)
            {
                houseMesh.uv.Add(unityMesh.uv[0]);
                houseMesh.uv.Add(unityMesh.uv[1]);
            }

            for(int i = 0 ; i < unityMesh.triangles.Count ; i++)
            {
                houseMesh.faces.Add(unityMesh.triangles[i]);
            }

            meshList.Add(houseMesh);
        }
    }

    void MakeMaterial() {
        List<HouseMaterial> material = objs.house.material;
    }

    void MakeLights() {

        obj[] obj = GameObject.FindGameObjectsWithTag("Light");
        foreach(object o in obj) {
            GameObject g = (GameObject) o;
            Lights l = new Lights();
            l.uid = g.name;
            l.src_position = new List<double>();
            l.src_position.Add(g.transform.position.x);
            l.src_position.Add(g.transform.position.y);
            l.src_position.Add(g.transform.position.z);
        }
    }
}
