using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Text _buttonText;
    
    public Manager calcManager
    {
        get
        {
            if (_calcManager == null)
                _calcManager = GetComponentInParent<Manager>();
            return _calcManager;
        }
    }
    static Manager _calcManager;
    
    public void OnTap()
    {
        calcManager.onButton(_buttonText.text[0]);
    }
}
