
using UnityEngine;
using UnityEngine.AI;

public class CombineSystem : MonoBehaviour
{
   [SerializeField]
    private EnemySpawner enemySpawner;

    private bool isMix = false;

    [SerializeField]
    private ObjectDataViewer dataViewer;

    public bool IsMix
    {
        get { return isMix; }
        set { isMix = value; }
    }

    public void Mix(GameObject unitA, GameObject unitB, GameObject unitC)
    {
        Vector3 position = new Vector2(unitA.transform.position.x, unitA.transform.position.y);
        Destroy(unitA);
        Destroy(unitB);
        GameObject newUnit = Instantiate(unitC,position,Quaternion.identity);
        int index = newUnit.name.IndexOf("(Clone)");
        if(index>0)
        {
            newUnit.name = newUnit.name.Substring(0 , index);
        }
        //movement = newUnit.GetComponent<Movement>();
        //movement.selectedAgent = null;
        //movement.selectedGameObject = null;
        newUnit.GetComponent<Weapon>().Setup(enemySpawner);
        dataViewer.OffPanel();

        isMix = true;
        if (CharacterSelector.Instance.SelectedCharacter == unitA ||
            CharacterSelector.Instance.SelectedCharacter == unitB)
        {
            CharacterSelector.Instance.DeselectCharacter();
        }
    }

    
}