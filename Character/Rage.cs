using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : MonoBehaviour
{
    //public float aSIncrease = 0.5f;
    public float activeChance = 0.3f;
    public float activeTime = 4f;
    private ObjectData objectData;
    private Weapon weapon;
    private ObjectDataViewer objectDataViewer;
    private bool isCoroutineRunning;
    public SpriteRenderer spriteRenderer;


    private void Start()
    {
        objectData = GetComponent<ObjectData>();
        weapon = GetComponent<Weapon>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    public void Update()

    {
        if (weapon.SkillActive && weapon.skills == Skills.Rage)
        {
            if(!isCoroutineRunning)
            { 
                weapon.SkillActive = false;
                StartCoroutine(ActivateRage());
            }
        }


    }

    private IEnumerator ActivateRage()
    {
        isCoroutineRunning = true;
        spriteRenderer.color = Color.red;
        float oriAttackSpeed = objectData.Rate;
        objectData.Rate = 2;
        if(objectDataViewer !=null)
        {
            objectDataViewer.UpdateUnitData();
        }
        else
        {
            objectDataViewer = GameObject.Find("CharterInformation").GetComponent<ObjectDataViewer>();
            objectDataViewer.UpdateUnitData();
        }

        yield return new WaitForSeconds(activeTime);

        objectData.Rate = oriAttackSpeed;
        if (objectDataViewer != null)
        {
            objectDataViewer.UpdateUnitData();
        }
        else
        {
            objectDataViewer = GameObject.Find("CharterInformation").GetComponent<ObjectDataViewer>();
            objectDataViewer.UpdateUnitData();
        }
        isCoroutineRunning=false;
        spriteRenderer.color = Color.white;
    }
}

