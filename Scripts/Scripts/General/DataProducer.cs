using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class DataProducer : MonoBehaviour
{
    public String[] stringsA = new String[12];
    public String[] stringsB = new String[12];
    public String[] stringsC = new String[12];
    public String[] stringsD = new String[12];
    public String[] stringsE = new String[12];
    public String[] stringsF = new String[12];
    void Awake()
    {
        List<Dictionary<string,object>> data = CSVReader.Read("CombineData");
        for (var i =0; i < data.Count; i++)
        {
            stringsA[i] = (string)data[i]["NameA"];
            stringsB[i] = (string)data[i]["NameB"];
            stringsC[i] = (string)data[i]["Result1"];
            stringsD[i] = (string)data[i]["NameD"];
            stringsE[i] = (string)data[i]["NameE"];
            stringsF[i] = (string)data[i]["Result2"];
        }
        
    }
}
