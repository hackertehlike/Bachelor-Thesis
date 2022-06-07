using System.Collections.Generic;

namespace HouseLayout {

public class Mesh {
 public string jid { get; set; }
 public string uid { get; set; }
 public List<double> xyz { get; set; }
 public List<double> normal { get; set; }
 public List<double> uv { get; set; }
 public List<int> faces { get; set; }
 public string material { get; set; }
 public string type { get; set; }
 public string constructid { get; set; }
 public string instanceid { get; set; }
}

}

