using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Debug.Log("checkpoint");
            PlayerPrefs.SetFloat("PosX", transform.position.x);
            PlayerPrefs.SetFloat("PosY", transform.position.y);
        }
    }
}
