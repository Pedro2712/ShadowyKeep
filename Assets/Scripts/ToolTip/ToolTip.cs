using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipManagement.Instance.SetAndShowToolTip(message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipManagement.Instance.HideToolTip();
    }

    public void close()
    {
        ToolTipManagement.Instance.HideToolTip();
    }
}
