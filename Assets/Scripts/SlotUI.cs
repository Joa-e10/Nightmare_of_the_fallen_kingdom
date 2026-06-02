using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public TextMeshPro _textname;
    public TextMeshPro _textamount;
    public Image _icon;
    void Start()
    {
        _textname = GameObject.Find("NameItem").GetComponent<TextMeshPro>();
        _textamount = GameObject.Find("QuantityItem").GetComponent<TextMeshPro>();
        _icon = GameObject.Find("IconItem").GetComponent<Image>();
    }


    void Update()
    {
        
    }
}
