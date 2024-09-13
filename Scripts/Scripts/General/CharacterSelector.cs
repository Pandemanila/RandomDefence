using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector Instance { get; private set; }

    private GameObject selectedCharacter;
    public GameObject SelectedCharacter { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectCharacter();
        }
        else if (Input.GetMouseButtonDown(1) && selectedCharacter != null)
        {
            MoveSelectedCharacter();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectCharacter();
        }
    }

    void SelectCharacter()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Character"))
        {
            selectedCharacter = hit.transform.gameObject;
        }
    }

    void MoveSelectedCharacter()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
        selectedCharacter.GetComponent<Movement>().SetDestination(targetPosition);
    }

    public void DeselectCharacter()
    {
        selectedCharacter = null;
    }
}