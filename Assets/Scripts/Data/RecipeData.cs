using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "Scriptable Objects/RecipeData")]
public class RecipeData : ScriptableObject
{
    public List<ItemData> _nameRequiredItem = new List<ItemData>();
    public List<int> _quantityRequiredItem = new List<int>();
    public int _levelRequired;
}
