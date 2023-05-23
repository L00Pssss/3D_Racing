using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
{
    [SerializeField] private RaceInfo m_raceInfo;

    [SerializeField] private Image m_icon;
    [SerializeField] private TextMeshProUGUI m_title;


    private void Start()
    {
        ApplyProperty(m_raceInfo);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);


        if (m_raceInfo == null) return;

        SceneManager.LoadScene(m_raceInfo.SceneName);
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;

        if(property is RaceInfo raceInfo == false) return;

        m_raceInfo = property as RaceInfo;

        m_icon.sprite = m_raceInfo.Icon;
        m_title.text = m_raceInfo.Title;
    }
}
