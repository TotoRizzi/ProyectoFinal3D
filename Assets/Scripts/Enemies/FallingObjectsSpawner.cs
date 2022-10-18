using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsSpawner : MonoBehaviour
{
    [SerializeField] GameObject _objectToFall;

    [SerializeField] float _spawnCd;
    float _currentSpawnCd;

    private void Update()
    {
        _currentSpawnCd += Time.deltaTime;

        if (_currentSpawnCd > _spawnCd)
        {
            Instantiate(_objectToFall, transform.position, Quaternion.identity);
            _currentSpawnCd = 0;
        }
    }
}
