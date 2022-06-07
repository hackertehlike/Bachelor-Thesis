using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using HouseMesh = HouseLayout.Mesh;
using HouseLayout;
using UnityMesh = UnityEngine.Mesh;

public class MeshSpawner : MonoBehaviour
{
    public House house;
    public List<HouseMesh> meshList;
    UnityMesh unityMesh;

    // Start is called before the first frame update
    void Start()
    {
        string jsonFilePath = @"Assets/0a9c667d-033d-448c-b17c-dc55e6d3c386.json";
        string json = File.ReadAllText(jsonFilePath);
        house = JsonConvert.DeserializeObject<House>(json);

        meshList = house.mesh;

        for(int i = 0; i < meshList.Count; i++) {
            SpawnMesh(i);
        }
    }

    void SpawnMesh(int meshIndex) {

        GameObject go = new GameObject();
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();

        HouseMesh houseMesh = meshList[meshIndex];

        // Read the mesh's vertices and faces and uv coordinates
        List<double> verticesList = houseMesh.xyz;
        Vector3[] vertices = ListToVec3Array(verticesList);
        // List<double> uvList = houseMesh.uv;
        // Vector2[] uv = ListToVec2Array(uvList);

        // Read the mesh's face indices
        int[] triangles = houseMesh.faces.ToArray();

        // Read the mesh's normals
        // List<double> normalsList = houseMesh.normal;
        // Vector3[] normals = ListToVec3Array(normalsList);

        // Create the mesh
        unityMesh = new UnityMesh();
        mf.GetComponent<MeshFilter>().mesh = unityMesh;
        unityMesh.vertices = vertices;
        unityMesh.triangles = triangles;
        go.name = houseMesh.uid;
        }


    Vector3[] ListToVec3Array(List<double> list) {

        Vector3[] arr = new Vector3[list.Count / 3];

        for (int i = 0; i < list.Count; i += 3) {
            arr[i / 3] = new Vector3((float)list[i], (float)list[i + 1], (float)list[i + 2]);
        }

        return arr;
    }

    Vector2[] ListToVec2Array(List<double> list) {

        Vector2[] arr = new Vector2[list.Count / 2];

        for (int i = 0; i < list.Count; i += 2) {
            arr[i / 2] = new Vector2((float)list[i], (float)list[i + 1]);
        }

        return arr;
    }


}
