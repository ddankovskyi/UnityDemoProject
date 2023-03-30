public class InventorySlot 
{
    public string TypeId;
    public InventoryItem Content;


    public class Item
    {
        public string id;
        public A value;
    }

    public class A { };
    public class B : A { };

    void DoStuff()
    {
        Item item1 = new Item();
        item1.value = new B();

        // serialize item
        Item item2 = // deserialize item1
        B bval = item2.value as B;
    }
}
