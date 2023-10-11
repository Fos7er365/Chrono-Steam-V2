using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtonSelectorHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] float verticalMovingAmount = 30f, movingTime = .1f;
    [Range(0f, 2f), SerializeField] float scaleAmount = 1.1f;

    Vector3 startPosition;
    Vector3 startScale;

    private void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    IEnumerator MoveCard(bool startingAnim)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float elapsedTime = 0f;
        while (elapsedTime < movingTime)
        {
            elapsedTime += Time.deltaTime;

            if (startingAnim)
            {
                endPosition = startPosition + new Vector3(0f, verticalMovingAmount, 0f);
                endScale = startScale * scaleAmount;
            }
            else
            {
                endPosition = startPosition;
                endScale = startScale;
            }

            Vector3 lerpedPos = Vector3.Lerp(transform.position, endPosition, (elapsedTime / movingTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / movingTime));

            transform.position = lerpedPos;
            transform.localScale = lerpedScale;

            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(true));
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(false));
    }
}
