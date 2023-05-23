using System;
using UnityEngine;

//public class DifficultySetting : Setting
//{
//    public enum DifficultyType
//    { }

//    public DifficultyType GetDifficulty()
//    {
//        return (DifficultyType)GetVelue();
//    }
//}

public abstract class Setting : ScriptableObject
{
    [SerializeField] protected string m_title;
    public string Title => m_title;

    public virtual bool isMinValue { get; }
    public virtual bool isMaxValue { get; }

    public virtual void SetNextValue() { }
    public virtual void SetPreviousValue() { }

    public virtual object GetVelue() { return default(object); }
    public virtual string GetStringValue() { return string.Empty; }

    public virtual void Apply() { }

    public virtual void Load()  { }
}
