using System.Collections;
using System.Collections.Generic;

namespace HouseLayout {

[System.Serializable]
public class House {
 public string uid { get; set; }
 public string jobid { get; set; }
 public string design_version { get; set; }
 public string code_version { get; set; }
 public List<int> north_vector { get; set; }
 public List<Furniture> furniture { get; set; }
 public List<Mesh> mesh { get; set; }
 public List<Material> material { get; set; }
 public List<Lights> lights { get; set; }
 public Extension extension { get; set; } 
 public Scene scene { get; set; } 
 public List<Groups> groups { get; set; }
 public List<string> materialList { get; set; }
 public string version { get; set; }

}

}