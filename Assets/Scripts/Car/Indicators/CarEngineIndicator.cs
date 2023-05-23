using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
class EnigneIndicatorColor
{
    public float MaxRpm;
    public Color color;
}

public class CarEngineIndicator : MonoBehaviour
{
    [SerializeField] private Car m_car;
    [SerializeField] Image m_EngineRpmImage;
    [SerializeField] private EnigneIndicatorColor[] colors;


    private void Update()
    {
        m_EngineRpmImage.fillAmount = m_car.EngineRpm / m_car.EngineMaxRpm;

        for (int i = 0; i < colors.Length; i++)
        {
            if (m_car.EngineRpm < colors[i].MaxRpm)
            {
                m_EngineRpmImage.color = colors[i].color;
                break;
            }
        }
    }

}
