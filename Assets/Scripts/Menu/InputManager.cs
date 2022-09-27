using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Dictionary<string, KeyCode> _buttonKeys;
    void Start()
    {
        _buttonKeys = new Dictionary<string, KeyCode>();

        _buttonKeys["Jump"] = KeyCode.Space;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool GetButtonDown(string buttonName)
    {
        if (!_buttonKeys.ContainsKey(buttonName)) return false;

        return Input.GetKeyDown(_buttonKeys[buttonName]);
    }
}
