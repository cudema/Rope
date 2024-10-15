using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestInfo", menuName = "ScriptableObjects/QuestInfo", order = 1)]
public class QuestInfo : ScriptableObject
{
    [field: SerializeField]
    public string id { get; private set; }

    [Header("일반 정보")]
    public string displayName;

    [Header("요구 조건")]
    public QuestInfo[] questPrerequisites;

    [Header("단계")]
    public GameObject[] questStepPrefabs;

    [Header("대화 시스템 인덱스")]
    public int dialogSystemIndex;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
