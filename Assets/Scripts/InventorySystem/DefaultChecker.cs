using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultChecker : ICompatibilityChecker
{

    public bool CheckCompatibility(string slotTypeId, string itemTypeId)
    {
        if (slotTypeId == null)
            return true;
        return slotTypeId.Equals(itemTypeId);
    }
}
