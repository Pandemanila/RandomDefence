using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTip2 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private ObjectDataViewer objectDataViewer;


    public void OnPointerEnter(PointerEventData eventData)
    {
        objectDataViewer.ShowToolTip2();

    }

    public void OnPointerExit(PointerEventData eventData)
    {        

        objectDataViewer.HideToolTip2();
    }


}
