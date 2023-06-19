
public class CharacterManager : IGlobalManager 
{
    CharacterData _data;

    public Character CurrentCharacter { get; set; }
   
    public int InventoryCapasity => _data.InventorySize;

    public void Init()
    {
        _data = new CharacterData();// TODO load _data from storage;
    }
}
