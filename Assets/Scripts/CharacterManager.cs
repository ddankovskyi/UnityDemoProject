
public class CharacterManager 
{
    CharacterData _data;

    public Character CurrentCharacter { get; set; }
   
    public int InventoryCapasity => _data.InventorySize;

    public CharacterManager(CharacterData characterData, Character character)
    {
        _data = characterData;
        CurrentCharacter = character;
    }
}
