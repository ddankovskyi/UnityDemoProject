using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICompatibilityChecker
{
    bool CheckCompatibility(string slotTypeId, string itemTypeId);
    
}
