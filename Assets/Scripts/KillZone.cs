using UnityEngine;
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
            player.TakeDamage(player.CurrentLife);
    }
}
