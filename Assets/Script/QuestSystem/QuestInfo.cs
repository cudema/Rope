using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestInfo", menuName = "ScriptableObjects/QuestInfo", order = 1)]
public class QuestInfo : ScriptableObject
{
    [field: SerializeField]
    public string id { get; private set; }

    [Header("�Ϲ� ����")]
    public string displayName;

    [Header("�䱸 ����")]
    public QuestInfo[] questPrerequisites;

    [Header("�ܰ�")]
    public GameObject[] questStepPrefabs;

    [Header("��ȭ �ý��� �ε���")]
    public int dialogSystemIndex;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
