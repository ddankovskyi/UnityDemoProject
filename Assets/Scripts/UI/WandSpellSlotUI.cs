using System;

public class WandSpellSlotUI : SlotUI
{
    Wand _wand;
    int _slotId;

    public Action<int, SpellItem> OnItemChanged; 

    public void Init(int slitId)
    {
        _slotId = slitId;
    }
    public override bool ReceiveItem(ItemUI itemUI)
    {
        if (itemUI.Item is SpellItem)
        {
            return base.ReceiveItem(itemUI);
        } else return false;
    }
    protected override void AcceptItem(ItemUI itemUI)
    {
        base.AcceptItem(itemUI);
        OnItemChanged?.Invoke(_slotId, ContainedItem.Item as SpellItem);
    }

    protected override void ReleaseSlot()
    {
        base.ReleaseSlot();
        OnItemChanged?.Invoke(_slotId, null);

    }
}
