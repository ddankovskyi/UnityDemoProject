using Assets.Scripts.Items;
using Newtonsoft.Json;
using System.Collections.Generic;

internal class WandItem : InventoryItem
{
    
    [JsonProperty] float castDelay;
    [JsonProperty] float rechargeTime;
    [JsonProperty] int spellsPerShoot;
    [JsonProperty] List<SpellItem> spells;
}

