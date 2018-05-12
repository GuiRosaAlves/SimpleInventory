using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory variable;
    private ItemStack[] constant = new ItemStack[16];
    public ItemStack[] inventory { get { return ((variable == null) ?  constant : variable.itemList); } }
    private Slot[] slots;
    public Item equippedItem { get { return _equippedItem; } }
    public Item _equippedItem;

    private void Awake()
    {
        GhostStack.Initialize(GameObject.FindWithTag("GhostStack"));

        slots = GetComponentsInChildren<Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            constant[i] = new ItemStack();
            inventory[i].id = i;
            slots[i].Initiate(inventory[i]);
        }
    }

    public static void MoveStacks(ItemStack a, ItemStack ghostStack, ItemStack b)
    {
        a.quantity -= GhostStack.stack.quantity;

        if (b.IsEmpty())
        {
            b.item = ghostStack.item;
            b.quantity = ghostStack.quantity;
        }
        else if (b.item.stackable && a.item == b.item)
        {
            b.item = ghostStack.item;
            b.quantity += ghostStack.quantity;
        }
        else
        {
            a.item = b.item;
            a.quantity = b.quantity;

            b.item = ghostStack.item;
            b.quantity = ghostStack.quantity;
        }
    }

    public void Add(Item item, int quantity)
    {
        if (item.stackable)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (item == inventory[i].item)
                {
                    inventory[i].quantity += quantity;
                    slots[inventory[i].id].stackedItem.UpdateUI();
                    return;
                }
            }
            foreach (ItemStack stack in inventory)
            {
                if (stack.IsEmpty())
                {
                    stack.item = item;
                    stack.quantity = quantity;
                    slots[stack.id].stackedItem.UpdateUI();
                    return;
                }
            }
        }
        else
        {
            foreach(ItemStack stack in inventory)
            {
                if (stack.IsEmpty())
                {
                    stack.item = item;
                    stack.quantity = quantity;
                    slots[stack.id].stackedItem.UpdateUI();
                    return;
                }
            }
        }
    }

    public void EquipItem(int index)
    {
        _equippedItem = inventory[index].item;
    }

    public void IO()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void IO(bool state)
    {
        gameObject.SetActive(state);
    }
}