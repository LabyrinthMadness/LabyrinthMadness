using UnityEngine;

public class Panel : MonoBehaviour
{
    public bool isActive = false;

    public virtual void SetText(string setText)
    {
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        //OnShowEvent.Invoke();
    }

    public virtual void Hide()
    {
        //OnHideEvent.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void Toggle()
    {
        if (isActive == false)
        {
            Show();
            isActive = true;
        }
        else
        {
            Hide();
            isActive = false;
        }
    }
}