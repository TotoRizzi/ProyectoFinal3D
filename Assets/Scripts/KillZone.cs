using UnityEngine;
public class KillZone : MonoBehaviour
{
    [SerializeField] Transform _checkpoint;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            PlayerPrefs.SetFloat("PosX", _checkpoint.position.x);
            PlayerPrefs.SetFloat("PosY", _checkpoint.position.y);
            player.TakeDamage(player.CurrentLife);
        }
    }
}
