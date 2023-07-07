using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIFloatDisplay
{
    public void UpdateValue(float value);
    public void SetMaxValue(float value);
   // public void SetIsPecentageModeOn(bool isInPercentageMode);
    
}
