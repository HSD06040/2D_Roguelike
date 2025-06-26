using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConditionPassiveEffectEditor : Editor
{
    SerializedProperty IsDiff;
    SerializedProperty passiveFunc;

    SerializedProperty isCondition;
    SerializedProperty statType;
    SerializedProperty value;
    SerializedProperty condition;

    SerializedProperty isObjectSpawn;
    SerializedProperty spawnObject;
    SerializedProperty delay;
    SerializedProperty spawnOffset;

    SerializedProperty isStatModifier;
    SerializedProperty modifierStat;
    SerializedProperty modifierValue;

    SerializedProperty passiveTriggerType;
    SerializedProperty duration;

    SerializedProperty chance;
    SerializedProperty isChance;

    private void OnEnable()
    {
        IsDiff = serializedObject.FindProperty("IsDiff");
        passiveFunc = serializedObject.FindProperty("passiveFunc");

        isCondition = serializedObject.FindProperty("IsCondition");
        statType = serializedObject.FindProperty("statType");
        value = serializedObject.FindProperty("value");
        condition = serializedObject.FindProperty("condition");

        isObjectSpawn = serializedObject.FindProperty("IsObjectSpawn");
        spawnObject = serializedObject.FindProperty("spawnObject");
        spawnOffset = serializedObject.FindProperty("spawnOffset");
        delay = serializedObject.FindProperty("delay");

        isStatModifier = serializedObject.FindProperty("IsStatModifier");
        modifierStat = serializedObject.FindProperty("modifierStat");
        modifierValue = serializedObject.FindProperty("modifierValue");

        passiveTriggerType = serializedObject.FindProperty("passiveTriggerType");
        duration = serializedObject.FindProperty("duration");

        isChance = serializedObject.FindProperty("IsChance");
        chance = serializedObject.FindProperty("chance");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(IsDiff);
        if(IsDiff.boolValue)
        {
            EditorGUILayout.PropertyField(passiveFunc);
            serializedObject.ApplyModifiedProperties();
            return;
        }        

        EditorGUILayout.PropertyField(passiveTriggerType);
        if ((PassiveTriggerType)passiveTriggerType.enumValueIndex == PassiveTriggerType.OnTime)
        {
            EditorGUILayout.PropertyField(duration);
        }

        EditorGUILayout.PropertyField(isCondition);
        if(isCondition.boolValue)
        {
            EditorGUILayout.PropertyField(statType);
            EditorGUILayout.PropertyField(value);
            EditorGUILayout.PropertyField(condition);
        }

        EditorGUILayout.PropertyField(isObjectSpawn);
        if (isObjectSpawn.boolValue)
        {
            EditorGUILayout.PropertyField(spawnObject);
            EditorGUILayout.PropertyField(spawnOffset);
            EditorGUILayout.PropertyField(delay);

            EditorGUILayout.PropertyField(isChance);

            if (isChance.boolValue)
            {
                EditorGUILayout.PropertyField(chance);
            }
        }

        EditorGUILayout.PropertyField(isStatModifier);
        if (isStatModifier.boolValue)
        {
            EditorGUILayout.PropertyField(modifierStat);
            EditorGUILayout.PropertyField(modifierValue);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
