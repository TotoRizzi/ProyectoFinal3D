using UnityEngine;
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            UIManager.Instance.defeatEvent();
    }
}
