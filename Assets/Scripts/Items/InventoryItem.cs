

using Newtonsoft.Json;
using UnityEngine;
[JsonObject(MemberSerialization.OptIn)]
public abstract class InventoryItem : IStackableItem
{
    [JsonProperty]
    public virtual string Id { get; set; }
    [JsonProperty]
    public int Amount { get; set; } = 1;

    public virtual int StackSize => 1;
  



}