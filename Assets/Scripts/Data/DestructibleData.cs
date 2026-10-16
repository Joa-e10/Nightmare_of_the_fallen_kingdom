using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestructibleData", menuName = "Scriptable Objects/DestructibleDrop_SO")]
public class DestructibleData : ScriptableObject
{
    public int maxHealth;
    public List<GameObject> itemsDrop = new List<GameObject>();
    public enum TypeDrop {None, Random};
    public TypeDrop typeDrop;
    public int dropItemQuantity;
}
