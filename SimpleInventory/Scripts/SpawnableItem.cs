using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableItem : Item
{
    protected GameObject gameObject;

    public SpawnableItem(string name, string imageURL, bool stackable) : base(name, imageURL, stackable)
    {

    }

    public virtual void Spawn(Vector3 spawnPosition)
    {
        //GameObject droppedItem = GameObject.Instantiate(prefab.gameObject, spawnPosition, Quaternion.identity);
        GameObject droppedItem = new GameObject(name + "Dropped");
        droppedItem.AddComponent<SpriteRenderer>().sprite = image;
        droppedItem.AddComponent<PickupObject>().Initiate(GhostStack.stack.item, GhostStack.stack.quantity);
        droppedItem.AddComponent<BoxCollider2D>().isTrigger = true;
    }
}