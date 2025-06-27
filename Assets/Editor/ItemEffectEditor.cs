//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(AccessoriesEffect), true)]
//public class ItemEffectEditor : Editor
//{
//    SerializedProperty isCondition;
//    SerializedProperty statType;
//    SerializedProperty value;
//    SerializedProperty condition;

//    SerializedProperty triggerTypes;

//    SerializedProperty intervals;

//    private void OnEnable()
//    {
//        serializedObject.Update();

//        isCondition = serializedObject.FindProperty("isCondition");
//        statType = serializedObject.FindProperty("statType");
//        value = serializedObject.FindProperty("value");
//        condition = serializedObject.FindProperty("condition");

//        triggerTypes = serializedObject.FindProperty("triggerTypes");

//        intervals = serializedObject.FindProperty("intervals");
//    }

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        EditorGUILayout.PropertyField(isCondition, true);
//        if(isCondition.boolValue)
//        {
//            EditorGUILayout.PropertyField(statType, true);
//            EditorGUILayout.PropertyField(value, true);
//            EditorGUILayout.PropertyField(condition, true);
//        }

//        EditorGUILayout.PropertyField(triggerTypes, true);

//        SerializedProperty listProp = triggerTypes;
//        for (int i = 0; i < listProp.arraySize; i++)
//        {
//            SerializedProperty element = listProp.GetArrayElementAtIndex(i);
//            PassiveTriggerType trigger = (PassiveTriggerType)element.enumValueIndex;

//            if (trigger == PassiveTriggerType.OnInterval)
//                EditorGUILayout.PropertyField(intervals, true);
//        }

//        serializedObject.ApplyModifiedProperties();
//    }
//}
