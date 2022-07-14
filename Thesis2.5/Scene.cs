using System.Collections;
using System.Collections.Generic;


public class Scene {
 public string @ref { get; set; }
 public List<double> pos { get; set; }
 public List<double> rot { get; set; }
 public List<double> scale { get; set; }
 public List<Room> room { get; set; }
 public BoundingBox boundingBox { get; set; } 

}