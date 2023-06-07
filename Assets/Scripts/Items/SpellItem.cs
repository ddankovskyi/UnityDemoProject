using Newtonsoft.Json;


public class SpellItem : InventoryItem
{
    [JsonProperty]
    public int ChargesLeft = 1;
    [JsonProperty]
    public int MaxCharges = 1;
}

