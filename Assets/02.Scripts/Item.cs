using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public int id;
    public string title;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    public Item(int id, string title, Dictionary<string,int> stats)
    {
        this.id = id;
        this.title = title;
        this.stats = stats;
        this.icon = Resources.Load<Sprite>("03.Sprites/Items/" + this.title);
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.stats = item.stats;
        this.icon = Resources.Load<Sprite>("03.Sprites/Items/" + this.title);

    }
}
