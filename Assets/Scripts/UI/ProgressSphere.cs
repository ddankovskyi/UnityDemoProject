using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressSphere : MonoBehaviour, IUIFloatDisplay
{
    [SerializeField] RawImage _rawImage;
    [SerializeField] string _materialParameterName = "_amount";
    Material _materialInstance;
    float _maxValue;
    float _value;


    private void Awake()
    {
        _materialInstance = new Material(_rawImage.material);
        _rawImage.material = _materialInstance;
    }

    public void SetMaxValue(float value)
    {
        _maxValue = value;
        // Debug.Log("max value set to " + _maxValue);
    }


    public void UpdateValue(float value)
    {
        _value = value;
        _materialInstance.SetFloat(_materialParameterName, _value/_maxValue);
    }

}
