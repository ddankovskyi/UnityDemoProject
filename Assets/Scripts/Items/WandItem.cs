using Newtonsoft.Json;
using System.Collections.Generic;

public class WandItem : InventoryItem
{
    [JsonProperty] public bool Shuffle = false;
    [JsonProperty] public float CastDelay;
    [JsonProperty] public float RechargeTime;
    [JsonProperty] public int SpellsPerShoot = 1;
    [JsonProperty] public float Manapool;
    [JsonProperty] public float ManaChargeSpeed;
    [JsonProperty] public List<SpellItem> Spells;
    public int Capasity => Spells.Count;

}
