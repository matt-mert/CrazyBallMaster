using UnityEngine;

public class JoystickMask : MonoBehaviour
{
    [SerializeField] private RectTransform outerTransform;
    [SerializeField] private RectTransform innerTransform;
    [SerializeField] private RectTransform maskTransform;

    private Vector2 initialMaskPos;

    private void Awake() => initialMaskPos = maskTransform.anchoredPosition;

    private void Update()
    {
        Vector2 outerAnchor = outerTransform.anchoredPosition;
        Vector2 innerAnchor = innerTransform.anchoredPosition;
        Vector2 maskAnchor = maskTransform.anchoredPosition;

        maskAnchor.x = innerAnchor.x;
        maskAnchor.y = innerAnchor.y;

        maskTransform.anchoredPosition = maskAnchor;

        outerAnchor.x = initialMaskPos.x - maskAnchor.x;
        outerAnchor.y = initialMaskPos.y - maskAnchor.y;

        outerTransform.anchoredPosition = outerAnchor;
    }
}
