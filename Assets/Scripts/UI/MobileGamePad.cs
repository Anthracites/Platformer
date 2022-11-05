using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;

public class MobileGamePad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler // Управление персонажем с помошью джостика
{
    public Image GamePad;
    public Image Knob;
    public Vector2 inputVector;

    void Start()
    {
        GamePad = gameObject.GetComponent<Image>();
        Knob = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

   public virtual void OnPointerDown(PointerEventData ped)
    {

    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        Knob.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GamePad.rectTransform, ped.position, ped.pressEventCamera, out pos))
            {
            pos.x = (pos.x / GamePad.rectTransform.sizeDelta.x);
            pos.y = (pos.y / GamePad.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            Knob.rectTransform.anchoredPosition = new Vector2(inputVector.x * (GamePad.rectTransform.sizeDelta.x/4), inputVector.y * (GamePad.rectTransform.sizeDelta.y/4));

        }
    }

    public float Horizontal()
    {
         if (inputVector.x != 0) return inputVector.x;
         else return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");
    }
}
