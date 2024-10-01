using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int salvage; //generic repair material, discuss in class
    [SerializeField] private int lootCurrency;

    private GameObject holding;
    [SerializeField] private List<DefenseScriptableBase> heldDefenses;
    // Start is called before the first frame update
    void Start()
    {
        heldDefenses = new List<DefenseScriptableBase>();
        holding = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetSalvageCount(){
        return salvage;
    }

    public void SetSalvageCount(int newCount){
        salvage = newCount;
    }

    public int getLootCurrency(){
        return lootCurrency;
    }

    public void setLootCurrency(int value){
        lootCurrency = value;
    }

    public GameObject GetHolding(){
        GameObject hold = holding;
        holding = null;
        return hold;
    }

    public bool IsHolding(){
        return holding != null;
    }

    public void SetHolding(GameObject toBeHeld){
        holding = toBeHeld;
    }
}
