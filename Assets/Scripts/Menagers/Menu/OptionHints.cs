using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionHints : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hint;
    private Vector3 mousepos;

    Ray ray;
    RaycastHit hit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "NickNameInput") {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Instantiate(hint, pos, Quaternion.identity);
        }
    }
    public void OnPointerExit(PointerEventData eventData) {

    }
}
