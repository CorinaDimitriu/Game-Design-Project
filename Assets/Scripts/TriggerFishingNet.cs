using UnityEngine;

public class TriggerFishingNet : MonoBehaviour
{
    public GameObject inventoryManager;
    public AudioSource MoneySource;

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Sph")
        {
            MoneySource.Play();
            inventoryManager.GetComponent<GeneralKeyboardActions>().enabledObjects[5] = true;
            this.gameObject.SetActive(false);
        }
    }
}
