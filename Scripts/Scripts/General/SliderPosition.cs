using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPosition : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance;
    private Transform targetTransform;
    private RectTransform rect;
    private Camera mainCamera;


    public void Awake()
    {
        mainCamera = Camera.main;
    }

    public void Setup(Transform target)
    {
        targetTransform = target;
        rect = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.parent as RectTransform, screenPosition, mainCamera, out localPoint);
        localPoint.y += distance.y +60f;
        rect.localPosition = localPoint;
    }
}
