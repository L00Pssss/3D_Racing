using UnityEngine;

public class SettingLoader : MonoBehaviour
{
    [SerializeField] private Setting[] allSettings;

    private void Awake()
    {
        foreach (var setting in allSettings)
        {
            setting.Load();
            setting.Apply();
        }
    }
}
