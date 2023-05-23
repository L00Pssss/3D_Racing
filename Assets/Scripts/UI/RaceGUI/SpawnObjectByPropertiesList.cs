using UnityEngine;

public class SpawnObjectByPropertiesList : MonoBehaviour
{
    [SerializeField] private Transform m_parent;
    [SerializeField] private GameObject m_prefab; // на объекте должен быть MonoBehaviour
    [SerializeField] private ScriptableObject[] m_properties;

    [ContextMenu(nameof(SpawnInEditMode))]
    public void SpawnInEditMode()
    {
        if (Application.isPlaying == true) return;

        GameObject[] allObject = new GameObject[m_parent.childCount]; // скопировали все объекты 

        for (int i = 0; i < m_parent.childCount; i++) 
        {
            allObject[i] = m_parent.GetChild(i).gameObject; // получили на все объекты 
        }

        for (int i = 0; i < allObject.Length; i++)
        {
            DestroyImmediate(allObject[i]); // удалили 
        }

        for (int i = 0; i < m_properties.Length; i++) 
        {
            GameObject button = Instantiate(m_prefab, m_parent);
            IScriptableObjectProperty scriptableObjectProperty = button.GetComponent<IScriptableObjectProperty>();
            scriptableObjectProperty.ApplyProperty(m_properties[i]);

        }

    }
}
