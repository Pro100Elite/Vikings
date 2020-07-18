using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModileController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler 
{

    private Image joistickBg;
    [SerializeField]
    private Image joistick;
    private Vector2 vector;


    void Start()
    {
        joistickBg = GetComponent<Image>();
        joistick = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joistickBg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joistickBg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joistickBg.rectTransform.sizeDelta.x);

            vector = new Vector2(pos.x * 2-1, pos.y * 2-1);
            vector = (vector.magnitude > 1.0f) ? vector.normalized : vector;

            joistick.rectTransform.anchoredPosition = new Vector2(vector.x *(joistickBg.rectTransform.sizeDelta.x/ 2), vector.y * (joistickBg.rectTransform.sizeDelta.y / 2));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        vector = Vector2.zero;
        joistick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        if (vector.x != 0)
        {
            return vector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vertical()
    {
        if (vector.y != 0)
        {
            return vector.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
