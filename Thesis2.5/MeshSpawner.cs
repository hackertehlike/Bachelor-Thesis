using System.Collections;
using System.Collections.Generic;
using System.IO;
using HouseLayout;
using Newtonsoft.Json;
using UnityEngine;

using HouseMaterial = HouseLayout.Material;
using HouseMesh = HouseLayout.Mesh;
using UnityMaterial = UnityEngine.Material;
using UnityMesh = UnityEngine.Mesh;

public class MeshSpawner : MonoBehaviour
{
    public HouseManager hm;

    House house;

    List<HouseMesh> meshList;

    Dictionary<string, string> material_uid_to_jid;

    void Start()
    {
        ReadAndLoad();

        for (int i = 0; i < meshList.Count; i++)
        {
            SpawnMesh (i);
        }
    }

    void ReadAndLoad()
    {
        // Deserialize the house layout file
        string house_id = hm.house_id;
        string jsonFilePath = @"Assets/" + house_id + ".json";
        string json = File.ReadAllText(jsonFilePath);
        house = JsonConvert.DeserializeObject<House>(json);

        // Get the list of meshes in the house
        meshList = house.mesh;

        // Save uid to jid mappings for materials
        material_uid_to_jid = new Dictionary<string, string>();

        for (int i = 0; i < house.material.Count; i++)
        {
            string uid = house.material[i].uid;
            string jid = house.material[i].jid;
            if (!material_uid_to_jid.ContainsKey(uid))
                material_uid_to_jid.Add(uid, jid);
        }
    }

    void SpawnMesh(int meshIndex)
    {
        GameObject go = new GameObject();
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();

        HouseMesh houseMesh = meshList[meshIndex];

        // Read the mesh's vertices and faces and uv coordinates
        List<double> verticesList = houseMesh.xyz;
        Vector3[] vertices = ListToVec3Array(verticesList);
        List<double> uvList = houseMesh.uv;
        Vector2[] uv = ListToVec2Array(uvList);

        // Read the mesh's face indices
        int[] triangles = houseMesh.faces.ToArray();

        // Read the mesh's normals
        List<double> normalsList = houseMesh.normal;
        Vector3[] normals = ListToVec3Array(normalsList);

        // Create the mesh
        UnityMesh unityMesh = new UnityMesh();
        mf.GetComponent<MeshFilter>().mesh = unityMesh;
        unityMesh.vertices = vertices;
        unityMesh.triangles = triangles;

        go.name = houseMesh.uid;
        unityMesh.uv = uv;

        //unityMesh.normals = normals;
        // Read the material
        string material_uid = houseMesh.material;
        string material_jid = material_uid_to_jid[material_uid];

        Texture2D texture =
            Resources.Load(material_jid + "/texture") as Texture2D;

        UnityMaterial material = new UnityMaterial(Shader.Find("Diffuse"));
        material.mainTexture = texture;
        mr.material = material;

        // Create the mesh's backside
        GameObject backside = new GameObject();
        MeshFilter backside_mf = backside.AddComponent<MeshFilter>();
        MeshRenderer backside_mr = backside.AddComponent<MeshRenderer>();

        UnityMesh backsideMesh = new UnityMesh();

        backside_mf.GetComponent<MeshFilter>().mesh = backsideMesh;
        backsideMesh.vertices = vertices;

        for (int i = 0; i < triangles.Length; i = i + 3)
        {
            int tmp = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = tmp;
        }

        backsideMesh.triangles = triangles;
        backsideMesh.uv = uv;
        backside.name = houseMesh.uid + "_backside";
        backside_mr.material = material;
    }

    Vector3[] ListToVec3Array(List<double> list)
    {
        Vector3[] arr = new Vector3[list.Count / 3];

        for (int i = 0; i < list.Count; i += 3)
        {
            arr[i / 3] =
                new Vector3((float) list[i],
                    (float) list[i + 1],
                    (float) list[i + 2]);
        }

        return arr;
    }

    Vector2[] ListToVec2Array(List<double> list)
    {
        Vector2[] arr = new Vector2[list.Count / 2];

        for (int i = 0; i < list.Count; i += 2)
        {
            arr[i / 2] = new Vector2((float) list[i], (float) list[i + 1]);
        }

        return arr;
    }
}
