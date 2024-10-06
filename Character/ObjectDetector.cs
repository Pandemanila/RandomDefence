
using UnityEngine;
using UnityEngine.Timeline;


public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private ObjectDataViewer dataViewer;
    private RaycastHit2D hit ; 
    public GameObject marker;
    private GameObject previousSelectedObject=null;
    public GameObject cloneObject;
    public bool onClick = false;
    [SerializeField]
    private GameManager gameManager;
    public bool isPause =false;





    void Update()
    {
        ClickUnit();
        ResetCode();
    }

    public void ClickUnit()
    {
        // 마우스 클릭을 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭된 위치의 캐릭터 검출
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if (hit.collider != null)
            {
                // 캐릭터를 클릭했을 때 패널 활성화
                if (hit.collider.CompareTag("Character"))
                {   
   
                    cloneObject = hit.transform.gameObject;
                     if(previousSelectedObject!=null && previousSelectedObject != cloneObject)
                     {
                        previousSelectedObject.transform.Find("Circle").gameObject.SetActive(false);
                     }


                    if(cloneObject != previousSelectedObject)
                    {
                        marker = hit.collider.transform.Find("Circle").gameObject;
                        onClick = true;
                        marker.SetActive(true);
                        dataViewer.OnPanel(hit.transform);
                        previousSelectedObject = cloneObject;
                    }
                    
                    
                }


            }
        }
    }
    public void ResetCode()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (onClick)
            {
                previousSelectedObject = null;
                cloneObject = null;
                onClick = false;
            }
            else if(!isPause)
            {
                gameManager.OffSetting();
                gameManager.OnGamePanel();
                isPause = true;
            }
            else if (isPause)
            {
                gameManager.OffPanel();
                isPause = false;
            }
        }
    }
}




