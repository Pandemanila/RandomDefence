using UnityEngine;
using UnityEngine.Events;

public class MixUnits : MonoBehaviour
{
    public UnityEvent<GameObject, GameObject, GameObject> onMixUnits;

    void Mix(GameObject unitA, GameObject unitB, GameObject unitC)
    {
        Vector2 position = new Vector2(unitA.transform.position.x, unitA.transform.position.y);
        Destroy(unitA);
        Destroy(unitB);
        GameObject character = Instantiate(unitC, position, Quaternion.identity);
    }
}