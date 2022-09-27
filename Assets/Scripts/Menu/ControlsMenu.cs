using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ControlsMenu : MonoBehaviour
{
    InputManager _inputManager;
    [SerializeField] GameObject _keyItemPrefab;
    [SerializeField] Transform _parentKeysItems;
    Dictionary<string, TextMeshProUGUI> _buttonToLabel;

    string _keyToRebind = null;
    void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
        string[] buttonNames = _inputManager.GetButtonNames();
        _buttonToLabel = new Dictionary<string, TextMeshProUGUI>();
        for (int i = 0; i < buttonNames.Length; i++)
        {
            string bn = buttonNames[i];
            var go = Instantiate(_keyItemPrefab);
            go.transform.SetParent(_parentKeysItems);
            go.transform.localScale = Vector3.one;

            TextMeshProUGUI text = go.transform.Find("ButtonName").GetComponent<TextMeshProUGUI>();
            text.text = bn;

            TextMeshProUGUI keyNameText = go.transform.Find("Button/KeyName").GetComponent<TextMeshProUGUI>();
            keyNameText.text = _inputManager.KeyNameForButton(bn);
            _buttonToLabel[bn] = keyNameText;

            Button keyBindButton = go.transform.Find("Button").GetComponent<Button>();
            keyBindButton.onClick.AddListener(() => StartRebindFor(bn));
        }
    }
    private void Update()
    {
        if(_keyToRebind != null)
        {
            if (Input.anyKey)
            {
                foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kc))
                    {
                        _inputManager.SetButtonForKey(_keyToRebind, kc);
                        _buttonToLabel[_keyToRebind].text = kc.ToString();
                        _keyToRebind = null;
                        break;
                    }
                }
            }
        }
    }
    void StartRebindFor(string buttonName)
    {
        _keyToRebind = buttonName;
    }
}

