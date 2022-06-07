using System.Collections.Generic;

public class Lights {
 public string uid { get; set; }
 public string nodeType { get; set; }
 public string roomId { get; set; }
 public List<double> src_position { get; set; }
 public List<double> direction { get; set; }
 public List<double> up_vector { get; set; }
 public int color_temperature { get; set; }
 public int multiplier { get; set; }
 public bool DoubleSided { get; set; }
 public bool skylightPortal { get; set; }
 public double size0 { get; set; }
 public double size1 { get; set; }
 public int type { get; set; }
 public bool enabled { get; set; }
 public int units { get; set; }
 public bool affectSpecular { get; set; }
 public string hostInstanceId { get; set; }

}