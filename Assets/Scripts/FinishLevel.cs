using UnityEngine;
public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
            UIManager.Instance.victoryEvent();
    }
}
