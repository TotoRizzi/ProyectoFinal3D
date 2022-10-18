using UnityEngine;
using UnityEngine.SceneManagement;
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
