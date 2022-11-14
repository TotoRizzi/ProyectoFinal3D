using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingTrap : MonoBehaviour
{
    [SerializeField] GameObject _trapModel;
    [SerializeField] float _fallingSpeed;

    private void Update()
    {
        _trapModel.transform.Rotate(transform.forward, _fallingSpeed * Time.deltaTime);
    }
}
