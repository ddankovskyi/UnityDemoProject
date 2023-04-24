public class WandSpellSlotUI : SlotUI
{
    Wand _wand;
    int _slotId;
    public void Init(ItemUI item, Wand wand, int slotId)
    {
        base.Init(item);
        _wand = wand;
        _slotId = slotId;
    }

    protected override void AcceptItem(ItemUI itemUI)
    {
        base.AcceptItem(itemUI);
        // notify wand
    }
}
