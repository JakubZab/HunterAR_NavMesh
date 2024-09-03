using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickHandle;
    private Vector2 inputVector;
    private Vector2 originalPosition;

    private void Start()
    {
        joystickHandle = transform.GetChild(0).GetComponent<RectTransform>();
        originalPosition = joystickHandle.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out position);

        position.x = (position.x / GetComponent<RectTransform>().sizeDelta.x);
        position.y = (position.y / GetComponent<RectTransform>().sizeDelta.y);

        inputVector = new Vector2(position.x * 2 - 1, position.y * 2 - 1);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        joystickHandle.anchoredPosition = new Vector2(inputVector.x * (GetComponent<RectTransform>().sizeDelta.x / 2),
                                                      inputVector.y * (GetComponent<RectTransform>().sizeDelta.y / 2));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = originalPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
}
