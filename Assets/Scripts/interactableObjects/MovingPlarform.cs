using UnityEngine;
public class MovingPlarform : MonoBehaviour
{
    [SerializeField] Transform[] _wayPoints;
    [SerializeField] float _speed = 2;

    GameObject _player;
    Rigidbody _myRb;
    IMovement _myMovement;

    private void Start()
    {
        _player = GameManager.instance.Player.gameObject;
        _myRb = GetComponent<Rigidbody>();
        _myMovement = new WayPointMovement(transform, _myRb, _speed, _wayPoints);
    }

    private void FixedUpdate()
    {
        _myMovement.Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entro");
        if(other.gameObject == GameManager.instance.Player.gameObject)
            _player.transform.SetParent(transform);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.Player.gameObject)
            _player.transform.SetParent(null);
    }
}
