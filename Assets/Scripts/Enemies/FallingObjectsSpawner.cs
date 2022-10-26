using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsSpawner : MonoBehaviour
{
    [SerializeField] float _spawnCd;
    float _currentSpawnCd;

    private void Update()
    {
        _currentSpawnCd += Time.deltaTime;

        if (_currentSpawnCd > _spawnCd)
        {
            FRY_FallingRock.Instance.pool.GetObject().SetPosition(transform.position);
            _currentSpawnCd = 0;
        }
    }
}
