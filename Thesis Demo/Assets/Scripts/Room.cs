using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Room {
 public string type { get; set; }
 public string instanceid { get; set; }
 public double size { get; set; }
 public List<double> pos { get; set; }
 public List<double> rot { get; set; }
 public List<double> scale { get; set; }
 public List<Children> children { get; set; }
 public int empty { get; set; }
}