using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectableButton : UIButton
{
    [SerializeField] private Image m_slectImage;

    public UnityEvent OnSelect;
    public UnityEvent OnUnSelect;

    public override void SetFocuse()
    {
        base.SetFocuse();

        m_slectImage.enabled = true;
        OnSelect.Invoke();
    }

    public override void SetUnFocuse()
    {
        base.SetUnFocuse();

        m_slectImage.enabled = false;
        OnUnSelect.Invoke();
    }
}
