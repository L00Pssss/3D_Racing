using UnityEngine;

public class SpawnObjectByPropertiesList : MonoBehaviour
{
    [SerializeField] private Transform m_parent;
    [SerializeField] private GameObject m_prefab; // �� ������� ������ ���� MonoBehaviour
    [SerializeField] private ScriptableObject[] m_properties;

    [ContextMenu(nameof(SpawnInEditMode))]
    public void SpawnInEditMode()
    {
        if (Application.isPlaying == true) return;

        GameObject[] allObject = new GameObject[m_parent.childCount]; // ����������� ��� ������� 

        for (int i = 0; i < m_parent.childCount; i++) 
        {
            allObject[i] = m_parent.GetChild(i).gameObject; // �������� �� ��� ������� 
        }

        for (int i = 0; i < allObject.Length; i++)
        {
            DestroyImmediate(allObject[i]); // ������� 
        }

        for (int i = 0; i < m_properties.Length; i++) 
        {
            GameObject button = Instantiate(m_prefab, m_parent);
            IScriptableObjectProperty scriptableObjectProperty = button.GetComponent<IScriptableObjectProperty>();
            scriptableObjectProperty.ApplyProperty(m_properties[i]);

        }

    }
}
