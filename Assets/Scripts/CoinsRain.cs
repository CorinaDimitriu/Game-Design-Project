using System.Collections;
using UnityEngine;

public class CoinsRain : MonoBehaviour
{
    public GameObject coinGoldPrefab;
    public GameObject coinSilverPrefab;
    public GameObject[] waypoints;
    public GameObject coin;
    private GameObject[,] coins = new GameObject[15, 35];
    private bool start = true;
    private bool falling = true;

    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("coin");
        StartCoroutine(ApplyForces());
    }

    IEnumerator ApplyForces()
    {
        int randomIndex = UnityEngine.Random.Range(0, waypoints.Length);
        coin.transform.position = waypoints[randomIndex].transform.position;
        coin.GetComponent<Rigidbody>().AddForce(Vector3.down * 3000f, ForceMode.Force);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(3f);
            for (int index = 0; index < waypoints.Length; index++)
            {
                coins[i, index] = Instantiate(coinSilverPrefab, waypoints[index].transform.position, coinSilverPrefab.transform.rotation);
                coins[i, index].GetComponent<Rigidbody>().AddForce(Vector3.down * 3000f, ForceMode.Force);
                coins[i, index].tag = "fall" + index.ToString();
            }
        }
        randomIndex = UnityEngine.Random.Range(0, waypoints.Length);
        coin.GetComponent<Rigidbody>().velocity = Vector3.zero;
        coin.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        coin.transform.position = waypoints[randomIndex].transform.position;
        start = false;
        falling = false;
    }

    private void Update()
    {
        if(!start && !falling)
        {
            StartCoroutine(Resume());
        }
    }

    IEnumerator Resume()
    {
        falling = true;
        coin.GetComponent<Rigidbody>().AddForce(Vector3.down * 3000f, ForceMode.Force);
        //for (int i = 0; i < 10; i++)
        //{
        //    yield return new WaitForSeconds(1.5f);
        //    for (int index = 0; index < waypoints.Length; index++)
        //    {
        //        coins[i, index].transform.position = waypoints[index].transform.position;
        //        coins[i, index].GetComponent<Rigidbody>().AddForce(Vector3.down * 3000f, ForceMode.Force);
        //    }
        //}
        int randomIndex = UnityEngine.Random.Range(0, waypoints.Length);
        coin.GetComponent<Rigidbody>().velocity = Vector3.zero;
        coin.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        coin.transform.position = waypoints[randomIndex].transform.position;
        yield return new WaitForSeconds(30f);
        falling = false;
    }
}
