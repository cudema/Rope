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

    [Header("���� ��ȭ �ε��� ���")]
    public bool isStartDialog;

    [Header("���� ��ȭ �ý��� �ε���")]
    public int StartdialogSystemIndex;

    [Header("���� ��ȭ �ý��� �ε���")]
    public int EnddialogSystemIndex;

    [Header("���� ��� ��� ����")]
    public bool isBlackOut;

    [Header("���� ��� ��� Ÿ�̹�")]
    public int BlackOutTiming = -1;

    [Header("�� �̵� ��� ��� ����")]
    public bool isMoveNextScene;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
