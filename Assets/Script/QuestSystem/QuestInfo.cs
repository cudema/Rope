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

    [Header("시작 대화 인덱스 사용")]
    public bool isStartDialog;

    [Header("시작 대화 시스템 인덱스")]
    public int StartdialogSystemIndex;

    [Header("종료 대화 시스템 인덱스")]
    public int EnddialogSystemIndex;

    [Header("암전 기능 사용 여부")]
    public bool isBlackOut;

    [Header("사진 표시 여부")]
    public bool isShowPhoto;

    [Header("화면에 띄울 사진(없으면 기본 null값)")]
    public Sprite photoSprite = null;

    [Header("씬 이동 기능 사용 여부")]
    public bool isMoveNextScene;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
