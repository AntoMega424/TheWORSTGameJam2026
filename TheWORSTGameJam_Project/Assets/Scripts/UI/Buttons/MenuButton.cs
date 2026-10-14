using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private enum ButtonType
    {
        PLAY,
        QUIT
    }

    [SerializeField] private ButtonType m_type = ButtonType.PLAY;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }


    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OnClick()
    {
        switch (m_type)
        {
            case ButtonType.PLAY: StartGame(); break;
            case ButtonType.QUIT: QuitGame(); break;
            default: break;
        }
    }
}
