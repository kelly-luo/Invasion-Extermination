using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    void BuildDatabase()
    {
        items = new List<Item>() {
            new Item(0, "AngryShavedCat", new Dictionary<string, int> { { "Power", 100 } }),
            new Item(1, "cat", new Dictionary<string, int> { { "Power", 50 } }),
            new Item(2, "furretLongNeck", new Dictionary<string, int> { { "Power", 9002 } }),
            new Item(3, "GentlemanPepe", new Dictionary<string, int> { { "Power", 150 } }),
            new Item(4, "huh", new Dictionary<string, int> { { "Power", 3 } }),
            new Item(5, "Swamp", new Dictionary<string, int> { { "Power", 69 } }),
            new Item(6, "YukiFish", new Dictionary<string, int> { { "Power", 420 } })};
    }

    public Item GetItem(int id)
    {
        return items[id];
    }

    public Item GetItem(string itemTitle)
    {
        return items.Find(item => item.title == itemTitle);
    }
    
}
