using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CombineOrder : MonoBehaviour
{
    [SerializeField]
    private ObjectDetector objectDetector;
    [SerializeField]
    private CombineSystem combineSystem;
    [SerializeField]
    private DataProducer dataProducer;
    [SerializeField]
    private GameObject[] prefabsA;
    [SerializeField]
    private GameObject[] prefabsB;
    [SerializeField]
    private ObjectDataViewer dataViewer;
    [SerializeField]
    private TextManager textManager;

    private GameObject necessaryObject;
    private GameObject resultObject;


    public void CombineA()
    {

            GameObject unitA = objectDetector.cloneObject;
            for(var i = 0; i < dataProducer.stringsA.Length; i++)
            {
                if(dataProducer.stringsA[i].Equals(unitA.name))
                {
                    necessaryObject = GameObject.Find(dataProducer.stringsB[i]);
                    resultObject = prefabsA[i];
                    GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(go => go.name == dataProducer.stringsA[i]).ToArray();
                    
                    if(necessaryObject != null)
                    {
                        if (necessaryObject.name.Equals(unitA.name) )
                        {   
                            if(unitA != necessaryObject)
                            {
                                combineSystem.Mix(unitA,necessaryObject,resultObject);
                                dataViewer.OffPanel();
                                break;
                            }
                            else
                            {
                                //string characterName = GameObject.Find(dataProducer.stringsB[i]).name;
                                //textManager.WarningCombine(characterName);
                            }

                    }
                        else 
                        {
                            combineSystem.Mix(unitA,necessaryObject,resultObject);
                            dataViewer.OffPanel();
                            break;
                        }
                    }
                    else
                    {
                        //string characterName = GameObject.Find(dataProducer.stringsB[i]).name;
                        //textManager.WarningCombine(characterName);
                    }


            }

            
            }

    }
        public void CombineB()
    {


            GameObject unitA = objectDetector.cloneObject;
            for(var i = 0; i < dataProducer.stringsA.Length; i++)
            {
                if(dataProducer.stringsD[i].Equals(unitA.name))
                {
                    necessaryObject = GameObject.Find(dataProducer.stringsE[i]);
                    resultObject = prefabsB[i];
                    if(necessaryObject != null)
                    {
                        combineSystem.Mix(unitA,necessaryObject,resultObject);
                        break;
                    }
                    else
                    {
                        //string characterName = GameObject.Find(dataProducer.stringsE[i]).name;
                        //textManager.WarningCombine(characterName);
                    }
                }

            }
            
        

    }



}
