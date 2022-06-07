using System.Collections.Generic;

public class Material {
 public string uid { get; set; }
 public string jid { get; set; }
 public string texture { get; set; }
 public string normaltexture { get; set; }
 public List<int> color { get; set; }
 public int seamWidth { get; set; }
 public bool useColor { get; set; }
 public List<double> normalUVTransform { get; set; }
 public List<string> contentType { get; set; }

}