using System.Collections.Generic;

[System.Serializable]
public class Children {
 public string @ref { get; set; }
 public string instanceid { get; set; }
 public List<double> pos { get; set; }
 public List<double> rot { get; set; }
 public List<double> scale { get; set; }

}
