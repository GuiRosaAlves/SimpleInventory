using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : ScriptableObject
{
    public Sprite image;
    public bool stackable;

    public Item(string name, string imageURL, bool stackable)
    {
        this.name = name;
        this.image = Resources.Load<Sprite>("/item/" + imageURL);
        this.stackable = stackable;
    }
}