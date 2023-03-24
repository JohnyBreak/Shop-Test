using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItemConfig", menuName = "Shop/ShopItemConfig")]
public class ShopItemConfig : ScriptableObject
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private List<PurchaseButtonPricePair> _purchaseButtonPricePairs;

    [SerializeField, Min(0)] private int _hour;
    [SerializeField, Range(0, 60)] private int _min;
    [SerializeField, Range(0, 60)] private int _sec;

    public ItemType ItemType => _itemType;
    public Sprite Icon => _icon;
    public string Name => _name;
    public List<PurchaseButtonPricePair> PurchaseButtonPricePairs => _purchaseButtonPricePairs;
    public int Hour => _hour;
    public int Min => _min;
    public int Sec => _sec;

}

[Serializable]
public enum ItemType 
{
    None,
    Hat,
    Boot,
    Jacket,
}

[Serializable]
public class PurchaseButtonPricePair 
{
    public BasePurchaseButtonConfig PurchaseButton;
    [Min(0)] public int Price;
}

//[CustomEditor(typeof(ShopItemConfig))]
//public class ShopItemConfigEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        var item = (ShopItemConfig)target;

//        SerializedObject serializedObject = new SerializedObject(item);

//        SerializedProperty hour = serializedObject.FindProperty("_hour");
//        SerializedProperty min = serializedObject.FindProperty("_min");
//        SerializedProperty sec = serializedObject.FindProperty("_sec");

//        EditorGUILayout.BeginHorizontal();
//        //EditorGUIUtility.fieldWidth = 100;
//        EditorGUILayout.PropertyField(hour);
//        //EditorGUILayout.IntField("time", item._hour);
//        //EditorGUIUtility.labelWidth = 15;
//        EditorGUILayout.PropertyField(min);
//        EditorGUILayout.PropertyField(sec);
//        //EditorGUILayout.IntField(":", item._min);
//        //EditorGUILayout.IntField(":", item._sec);
//        EditorGUILayout.EndHorizontal();
//    }
//}