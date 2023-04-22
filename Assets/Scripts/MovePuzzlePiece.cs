using UnityEngine;

public class MovePuzzlePieces : MonoBehaviour
{
    public GameObject CorrectPosition;
    private Vector3 InitialPosition;
    private bool IsMoving;
    private float StartPosX;
    private float StartPosY;
    private bool IsPlaced;
    private static int Count;

    void Start()
    {
        InitialPosition = transform.localPosition;
        Count = 0;
    }

    void Update()
    {
        if (!IsPlaced)
        {
            if (IsMoving)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                gameObject.transform.localPosition = new Vector3(mousePos.x - StartPosX, mousePos.y - StartPosY, gameObject.transform.localPosition.z);
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse down");
            IsMoving = true;

            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            StartPosX = mousePos.x - transform.localPosition.x;
            StartPosY = mousePos.y - transform.localPosition.y;
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("mouse up");

        IsMoving = false;

        if (Mathf.Abs(transform.localPosition.x - CorrectPosition.transform.localPosition.x) <= 0.5f &&
            Mathf.Abs(transform.localPosition.y - CorrectPosition.transform.localPosition.y) <= 0.5f)
        {
            transform.position = new Vector3(CorrectPosition.transform.position.x, CorrectPosition.transform.position.y, CorrectPosition.transform.position.z);
            IsPlaced = true;
            Count++;
            //if(Count == 5)
            //{
                //final photo + rotate to see date
            //}
        }
        else
        {
            transform.localPosition = new Vector3(InitialPosition.x, InitialPosition.y, InitialPosition.z);
        }
    }
}
