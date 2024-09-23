using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

namespace RadioSilence.InventorySystem.Data
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [TextArea]
        [SerializeField] private string _description;
        [SerializeField] private float _mass;
        [SerializeField] private bool _isStackable;
        [SerializeField] private int _stackSize;
        [EnumFlags] [SerializeField] private ActionsWithItem _actions;
        [SerializeField] private Sprite _icon;

        public string ItemID => name;
        public string ItemName => _itemName;
        public string Description => _description;
        public float Mass => _mass;
        public bool IsStackable => _isStackable;
        public int StackSize => _stackSize;
        public ActionsWithItem Actions => _actions;
        public Sprite Icon => _icon;
    }

    [System.Flags]
    public enum ActionsWithItem
    {
        Use,
        Drop,
        Unload,
    }

    public sealed class EnumFlagsAttribute : PropertyAttribute
    {
        public EnumFlagsAttribute() { }

        public static List<int> GetSelectedIndexes<T>(T val) where T : IConvertible
        {
            List<int> selectedIndexes = new List<int>();
            for (int i = 0; i < System.Enum.GetValues(typeof(T)).Length; i++)
            {
                int layer = 1 << i;
                if ((Convert.ToInt32(val) & layer) != 0)
                {
                    selectedIndexes.Add(i);
                }
            }
            return selectedIndexes;
        }

        public static List<string> GetSelectedStrings<T>(T val) where T : IConvertible
        {
            List<string> selectedStrings = new List<string>();
            for (int i = 0; i < Enum.GetValues(typeof(T)).Length; i++)
            {
                int layer = 1 << i;
                if ((Convert.ToInt32(val) & layer) != 0)
                {
                    selectedStrings.Add(Enum.GetValues(typeof(T)).GetValue(i).ToString());
                }
            }
            return selectedStrings;
        }

        public static bool HasFlag<T, Y>(T enumerable, Y searchableFlag) where T : IConvertible where Y : IConvertible
        {
            List<string> availableActions = GetSelectedStrings(enumerable);

            return availableActions.Contains(searchableFlag.ToString());
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
        }
    }
#endif
}