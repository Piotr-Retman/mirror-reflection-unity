using System;
using System.Collections;
using System.Collections.Generic;
[Serializable]
public class LevelDefinition
{   
    public List<FieldCoords> question;
    public List<FieldCoords> answer;
    public int chances;
    public int path;
    public int levelNo;
        
}
