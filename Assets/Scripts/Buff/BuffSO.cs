using UnityEngine;

[CreateAssetMenu(fileName = "BuffSO", menuName = "Buff")]
public class BuffSO : ScriptableObject
{
    public string buffName;     // 버프 이름
    public string description;  // 버프 설명
    public string statName;     // 버프가 적용될 스탯 이름 (예: "health", "attackPower" 등)
    public float duration;      // 버프 지속 시간
    public float effectValue;   // 버프 효과 값
    public Sprite icon;         // 버프 아이콘
}