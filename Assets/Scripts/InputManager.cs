using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class InputManager : MonoBehaviour
{
    Dictionary<string, KeyCode> _buttonKeys;
    private void OnEnable()
    {
        _buttonKeys = new Dictionary<string, KeyCode>();

        _buttonKeys["Right"] = KeyCode.D;
        _buttonKeys["Left"] = KeyCode.A;
        _buttonKeys["Up"] = KeyCode.W;
        _buttonKeys["Down"] = KeyCode.S;
        _buttonKeys["Jump"] = KeyCode.Space;
        _buttonKeys["Dash"] = KeyCode.LeftShift;
        _buttonKeys["Attack"] = KeyCode.J;
        _buttonKeys["Throw"] = KeyCode.K;
    }
    public bool GetButtonDown(string buttonName)
    {
        if (!_buttonKeys.ContainsKey(buttonName)) return false;

        return Input.GetKeyDown(_buttonKeys[buttonName]);
    }
    public bool GetButton(string buttonName)
    {
        if (!_buttonKeys.ContainsKey(buttonName)) return false;

        return Input.GetKey(_buttonKeys[buttonName]);
    }
    public string[] GetButtonNames()
    {
        return _buttonKeys.Keys.ToArray();
    }
    public string KeyNameForButton(string buttonName)
    {
        if (!_buttonKeys.ContainsKey(buttonName)) return "N/A";

        return _buttonKeys[buttonName].ToString();
    }
    public void SetButtonForKey(string buttonName, KeyCode keyCode)
    {
        _buttonKeys[buttonName] = keyCode;
    }
    public float GetAxisRaw(string axis)
    {
        if (axis == "Horizontal")
            return GetButton("Right") ? 1f : GetButton("Left") ? -1f : 0;
        else if (axis == "Vertical")
            return GetButton("Up") ? 1f : GetButton("Down") ? -1f : 0;
        else return default;
    }
}
