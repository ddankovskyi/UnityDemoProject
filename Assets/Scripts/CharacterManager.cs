
public class CharacterManager : IGlobalManager 
{
    CharacterData _data;
   
    public int InventorySize => _data.InventorySize;

    public void Init()
    {
        _data = new CharacterData();// TODO load _data from storage;
    }
}
