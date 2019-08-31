using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public Image startImage;

    private void Update()
    {
        startImage.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
    }
}
