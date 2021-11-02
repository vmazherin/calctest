using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    [SerializeField] Text _numbers;
    [SerializeField] Text _operator;
    bool errorDisplayed;
    bool displayValid;
    bool specialAction;
    double currentVal;
    double storedVal;
    double result;
    char storedOperator;


    void Start()
    {
        onButton('c');
    }


    void calcClear ()
    {
        _numbers.text = "0";
        _operator.text = "";
        specialAction = displayValid = errorDisplayed = false;
        currentVal = result = storedVal = 0;
        storedOperator = ' ';
    }


    void numbersUpdate ()
    {
        if (!errorDisplayed)
        {
            _numbers.text = currentVal.ToString().Replace(',','.');
        }
        displayValid = false; 
        
    }

    void resultCalc (char activeOp)
    {
        switch (activeOp)
        {
            case '=':
                result = currentVal;
                break;
            case '+':
                result = storedVal + currentVal;
                break;
            case '-':
                result = storedVal - currentVal;
                break;
            case 'x':
                result = storedVal * currentVal;
                break;
            case '÷':
                if(currentVal != 0)
                {
                    result = storedVal / currentVal;
                }
                else
                {
                    errorDisplayed = true;
                    _numbers.text = "ERROR";
                }
                break;
            default:
                Debug.Log("unknown: " + activeOp);
                break;
        }
        currentVal = result;
        numbersUpdate();
    }
    public void onButton(char caption)
    {
        if(errorDisplayed)
        {
            calcClear();
        }
        if((caption >= '0' && caption <= '9') || caption == '.')
        {
            if(_numbers.text.Length < 15 || !displayValid)
            {
                if(!displayValid)
                {
                    _numbers.text = (caption == '.' ? "0" : "");
                }
                else if(_numbers.text == "0" && caption != '.')
                {
                    _numbers.text = "";
                }
                if (caption == '.')
                {
                    if (!_numbers.text.Contains(".")) {
                        _numbers.text += caption;
                    }
   
                } else { _numbers.text += caption; } 
               
                displayValid = true;
            }
        }
         else if(caption == 'c')
        {
            calcClear();
        }
        else if(caption == '±')
        {
            currentVal = -double.Parse(_numbers.text,CultureInfo.InvariantCulture);
            numbersUpdate();
            specialAction = true;
        }
        else if(caption == '%')
        {
            currentVal = storedVal * (double.Parse(_numbers.text) / 100d);
            numbersUpdate();
            specialAction = true;
        }
        else if(displayValid || storedOperator == '=' || specialAction)
        {
            currentVal = double.Parse(_numbers.text, CultureInfo.InvariantCulture);
            displayValid = false;
            if(storedOperator != ' ')
            {
                resultCalc(storedOperator);
                storedOperator = ' ';
            }
            _operator.text = caption.ToString();
            storedOperator = caption;
            storedVal = currentVal;
            numbersUpdate();
            specialAction = false;
        }
        else if (caption == '+' || caption == '-' || caption == 'x' || caption == '/')
        {
            storedOperator = caption;
            _operator.text = caption.ToString();
        }
    }
}
