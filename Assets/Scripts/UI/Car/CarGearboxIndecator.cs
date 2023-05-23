using System;
using TMPro;
using UnityEngine;

public class CarGearboxIndecator : MonoBehaviour
{
    [SerializeField] Car m_car;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        m_car.GearChanged += OnGearChanged;
    }

    private void OnDestroy()
    {
        m_car.GearChanged -= OnGearChanged;
    }
    private void OnGearChanged(string gearName)
    {
        textMeshProUGUI.text = gearName;
    }
}
