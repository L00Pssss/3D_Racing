using UnityEngine;
using TMPro;

public class CarSpeedIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_speedText;
    [SerializeField] Car m_car;
    private int speedCar;
    //private Vector2 Stock = new(0f, -3.9f);
    //private Vector2 FromToTwo = new(27.8f, -3.9f);
    //private Vector2 FromToOne = new(55f, -3.9f);

    void Update()
    {
        speedCar = ((int)m_car.LinerVelocity);

        m_speedText.text = speedCar.ToString(); // ToString("F0");

        //if (speedCar >= 0 && speedCar <= 9)
        //{
        //    m_speedText.rectTransform.localPosition = FromToOne;
        //}
        //if (speedCar >= 10 && speedCar <= 99)
        //{
        //    m_speedText.rectTransform.localPosition = FromToTwo;
        //}
        //if (speedCar >= 100)
        //{
        //    m_speedText.rectTransform.localPosition = Stock;
        //}      
    }
}