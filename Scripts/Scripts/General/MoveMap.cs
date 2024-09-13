using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveMap : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField]
    private ObjectDetector detector;
    //private Movement movement;
    private Vector3 story = new Vector3(0.5f, -14, 0);
    private Vector3 field = new Vector3(0.5f, 0, 0);
    private Movement movement;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private ObjectDataViewer objectDataViewer;

    public void Awake()
    {
        mainCamera = Camera.main;
    }

    public void FieldCamera()
    {
        mainCamera.transform.position = new Vector3(0.5f, 0, -10);
    }

    public void StoryCamera()
    {
        mainCamera.transform.position = new Vector3(0.5f, -14, -10);
    }

    public void MoveToField()
    {
        GameObject originCharacter = detector.cloneObject;
        movement = originCharacter.GetComponent<Movement>();
        if (!movement.isField)
        {
            Destroy(detector.cloneObject);
            GameObject character = Instantiate(originCharacter, field, Quaternion.identity);
            Activation(character);
            character.GetComponent<Movement>().isField = true;
            int index = character.name.IndexOf("(Clone)");
            if (index > 0)
            {
                character.name = character.name.Substring(0, index);
            }
            character.GetComponent<Weapon>().Setup(enemySpawner);
            objectDataViewer.OffPanel();
        }
        else
        {
            Debug.Log("이동할 수 없습니다.");
        }
    }

    public void MoveToStory()
    {
        GameObject originCharacter = detector.cloneObject;
        movement = originCharacter.GetComponent<Movement>();
        if (movement.isField)
        {
            Destroy(detector.cloneObject);
            GameObject character = Instantiate(originCharacter, story, Quaternion.identity);
            Activation(character);
            character.GetComponent<Movement>().isField = false;
            int index = character.name.IndexOf("(Clone)");
            if (index > 0)
            {
                character.name = character.name.Substring(0, index);
            }
            character.GetComponent<Weapon>().Setup(enemySpawner);
            objectDataViewer.OffPanel();
        }
        else
        {
            Debug.Log("이동할 수 없습니다.");
        }
        
    }
    public void Activation(GameObject character)
    {
        character.GetComponent<Movement>().enabled = true;
        character.GetComponent<BoxCollider2D>().enabled = true;
        character.GetComponent<Weapon>().enabled = true;
        character.GetComponent<Animator>().enabled = true;
        character.GetComponent<ObjectData>().enabled = true;
        character.GetComponent<NavMeshAgent>().enabled = true; 
        character.GetComponent<AudioSource>().enabled = true;
    }

    public void DefaultSpeed()
    {
        Time.timeScale = 1.0f;
    }

    public void FastSpeed()
    {
        Time.timeScale = 2.0f;
    }
}
