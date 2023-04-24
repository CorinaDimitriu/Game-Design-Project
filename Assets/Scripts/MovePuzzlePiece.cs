using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovePuzzlePiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Canvas Canvas;
    [SerializeField]
    private GameObject CorrectPosition;
    private Vector2 InitialPosition;
    private bool IsMoving;
    private bool IsPlaced;
    private static int Count = 0;
    private float x, y;
    [SerializeField]
    private GameObject FPSController;

    void Start()
    {
        InitialPosition = GetComponent<RectTransform>().anchoredPosition;
        switch(gameObject.name)
        {
            case "Piece1": x = 37.5f; y = 24.5f; break;
            case "Piece2": x = 36; y = 26f; break;
            case "Piece3": x = 37; y = 19; break;
            case "Piece4": x = 37.5f; y = 25; break;
            case "Piece5": x = 35.8f; y = 25.5f; break;
        }
    }

    void Update()
    {
        if (!IsPlaced)
        {
            if (IsMoving)
            {
                RectTransform rectTransform = GetComponent<RectTransform>();
                Vector2 position;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    (RectTransform)Canvas.transform,
                    Input.mousePosition,
                    null,
                    out position);
                if (position.x >= -x && position.x <= x && position.y >= -y && position.y <= y)
                {
                    rectTransform.anchoredPosition = position;
                }
                else if ((position.x < -x || position.x > x) && (position.y >= -y && position.y <= y))
                {
                    rectTransform.anchoredPosition = new Vector2(Mathf.Sign(position.x) * x, position.y);
                }
                else if ((position.x >= -x && position.x <= x) && (position.y < -y || position.y > y))
                {
                    rectTransform.anchoredPosition = new Vector2(position.x, Mathf.Sign(position.y) * y);
                }
                else
                {
                    rectTransform.anchoredPosition = new Vector2(Mathf.Sign(position.x) * x, Mathf.Sign(position.y) * y);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsMoving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsMoving = false;

        if (Mathf.Abs(transform.localPosition.x - CorrectPosition.transform.localPosition.x) <= 1f &&
            Mathf.Abs(transform.localPosition.y - CorrectPosition.transform.localPosition.y) <= 1f)
        {
            transform.localPosition = new Vector3(CorrectPosition.transform.localPosition.x, CorrectPosition.transform.localPosition.y, CorrectPosition.transform.localPosition.z);
            IsPlaced = true;
            Count++;
            if (Count == 1)
            {
                Canvas.transform.Find("Piece1").gameObject.SetActive(false);
                Canvas.transform.Find("Piece2").gameObject.SetActive(false);
                Canvas.transform.Find("Piece3").gameObject.SetActive(false);
                Canvas.transform.Find("Piece4").gameObject.SetActive(false);
                Canvas.transform.Find("Piece5").gameObject.SetActive(false);
                GameObject family = Canvas.transform.Find("Family").gameObject;
                family.SetActive(true);
                float scaleX = family.transform.localScale.x;
                family.transform.LeanMoveLocal(new Vector2(0, 0), 1f);
                family.transform.LeanScale(new Vector2(0.76f, 0.65f), 1f);
                family.transform.LeanScaleX(0, 1f).delay = 2f;
                Canvas.transform.Find("Back").gameObject.SetActive(true);
                Canvas.transform.Find("Back").LeanScaleX(0.76f, 1f).delay = 3f;
                FPSController.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
            }
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = InitialPosition;
        }
    }
}
