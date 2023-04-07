using Newtonsoft.Json;

namespace Assets.Scripts.Items
{
    internal class SpellItem : InventoryItem
    {
        [JsonProperty]
        public int ChargesLeft;
        [JsonProperty]
        public int MaxCharges;


    }
}
