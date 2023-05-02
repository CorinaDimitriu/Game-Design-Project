using UnityEngine;
using UnityEngine.UI;

public class PaintingInteraction : MonoBehaviour
{
    public GameObject player;
    public Text interactionKey;
    public Text interactionMessage;
    public float interactionDistance = 2.0f;
    private bool isInRange;
    private bool isPickedUp = false;

    public GameObject[] frames = new GameObject[5];
    public int currentFrameIndex;
    private Material pickedUpPaintingMaterial;
    public Material woodMaterial;

    public GameObject[] paintings;

    void Update()
    {
        float paintingDistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (!IsPaintingTheClosest(paintingDistanceToPlayer))
        {
            return;
        }

        bool playerCloseToPainting = paintingDistanceToPlayer <= interactionDistance;

        bool playerCloseToFrame = false;
        for (int i = 0; i < frames.Length; i++)
        {
            float playerDistanceToFrame = Vector3.Distance(player.transform.position, frames[i].transform.position);
            if (playerDistanceToFrame <= interactionDistance)
            {
                currentFrameIndex = i;
                playerCloseToFrame = true;
                break;
            }
        }

        //Debug.Log("isPickedUp = "); Debug.Log(isPickedUp);
        //Debug.Log("closeToPainting = "); Debug.Log(closeToPainting);
        //Debug.Log("closeToFrame = "); Debug.Log(closeToFrame);
        //Debug.Log("currentFrameIndex = "); Debug.Log(currentFrameIndex);
        //Debug.Log("FrameHasPainting = "); Debug.Log(FrameHasPainting(frames[currentFrameIndex]));

        if (!isPickedUp && playerCloseToPainting)
        {
            if (!isInRange)
            {
                isInRange = true;
                interactionKey.gameObject.SetActive(true);
                interactionMessage.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpPainting();
            }
        }
        else if (isPickedUp && playerCloseToFrame && !FrameHasPainting(frames[currentFrameIndex]))
        {
            if (!isInRange)
            {
                isInRange = true;
                interactionKey.gameObject.SetActive(true);
                interactionMessage.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlacePaintingOnFrame();
            }
        }
        else if (!isPickedUp && playerCloseToFrame && FrameHasPainting(frames[currentFrameIndex]))
        {
            //Debug.Log(transform.parent == frames[currentFrameIndex].transform.parent.gameObject);
            //Debug.Log(transform.parent);
            //Debug.Log(frames[currentFrameIndex].transform.parent.gameObject);
            if (!isInRange)
            {
                isInRange = true;
                interactionKey.gameObject.SetActive(true);
                interactionMessage.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpPainting();
            }
        }
        else
        {
            if (!playerCloseToPainting && !playerCloseToFrame && isInRange)
            {
                isInRange = false;
                interactionKey.gameObject.SetActive(false);
                interactionMessage.gameObject.SetActive(false);
            } 
            if (isPickedUp && Input.GetKeyDown(KeyCode.E))
            {
                DropPainting();
            }
        }
    }

    void PickUpPainting()
    {
        isPickedUp = true;
        pickedUpPaintingMaterial = GetComponent<MeshRenderer>().material;
        //GetComponent<MeshRenderer>().enabled = true;

        // Check if the painting is on a frame
        if (transform.parent != null)
        {
            // Reset the frame material
            MeshRenderer frameRenderer = frames[currentFrameIndex].GetComponent<MeshRenderer>();
            Material[] frameMaterials = frameRenderer.materials;
            frameMaterials[1] = woodMaterial;
            frameRenderer.materials = frameMaterials;
        }

        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0, -0.7f, 1);
        transform.localRotation = Quaternion.Euler(0, 180, 0);

        interactionKey.gameObject.SetActive(false);
        interactionMessage.gameObject.SetActive(false);
    }

    void PlacePaintingOnFrame()
    {
        if (pickedUpPaintingMaterial != null)
        {
            isPickedUp = false;
            MeshRenderer frameRenderer = frames[currentFrameIndex].GetComponent<MeshRenderer>();
            Material[] frameMaterials = frameRenderer.materials;
            frameMaterials[1] = pickedUpPaintingMaterial;
            frameRenderer.materials = frameMaterials;

            // Reset the picked-up painting material
            pickedUpPaintingMaterial = null;

            //Debug.Log("Placing painting on frame no. ");
            //Debug.Log(currentFrameIndex);

            // Detach the painting from the player and attach it to the frame as a child and position it correctly
            transform.SetParent(frames[currentFrameIndex].transform.parent.gameObject.transform);
            transform.localPosition = new Vector3(0, 0, 0.1f);
            transform.localRotation = Quaternion.Euler(0, 180, 0);

            interactionKey.gameObject.SetActive(false);
            interactionMessage.gameObject.SetActive(false);
        }
    }

    void DropPainting()
    {
        if (pickedUpPaintingMaterial != null)
        {
            isPickedUp = false;

            // Detach the painting from the player
            transform.SetParent(null);

            // Position the painting just in front of the player
            transform.position = player.transform.position + player.transform.forward * 0.5f;
            // Lower the painting a bit so it's not floating in the air
            transform.position = new Vector3(transform.position.x, transform.position.y - 1.3f, transform.position.z);
            transform.rotation = Quaternion.Euler(90, 180, 0);

            // Reset the picked-up painting material
            pickedUpPaintingMaterial = null;
        }
    }

    bool FrameHasPainting(GameObject frameMeshFilter)
    {
        // get the parent of the frameMeshFilter
        GameObject frame = frameMeshFilter.transform.parent.gameObject;

        // check if the frame has more than 1 child
        if (frame.transform.childCount > 1 && currentFrameIndex != 4)
        {
            return true;
        }
        else if (frame.transform.childCount > 2 && currentFrameIndex == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsPaintingTheClosest(float paintingDistanceToPlayer)
    {
        // Get the minimum distance to the player for all paintings
        for (int i = 0; i < paintings.Length; i++)
        {
            float anotherDistance = Vector3.Distance(player.transform.position, paintings[i].transform.position);
            if (anotherDistance < paintingDistanceToPlayer)
            {
                return false;
            }
        }
        return true;
    }
}
