using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : MonoBehaviour
{
    [SerializeField]
    Text messageTxt;

    public void Popup(string message)
    {
        gameObject.SetActive(true);
        messageTxt.text = message;
    }

    public void Ok()
    {
        gameObject.SetActive(false);
    }
}