using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private ObjectDataViewer objectDataViewer;


    public void OnPointerEnter(PointerEventData eventData)
    {
        objectDataViewer.ShowToolTip();

    }

    public void OnPointerExit(PointerEventData eventData)
    {        

        objectDataViewer.HideToolTip();
    }


}
