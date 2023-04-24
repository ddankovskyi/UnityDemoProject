using Newtonsoft.Json;


public class SpellItem : InventoryItem
{
    [JsonProperty]
    public int ChargesLeft;
    [JsonProperty]
    public int MaxCharges;
}

