using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;

public class TextBox : MonoBehaviour
{
    private TextMeshProUGUI m_textUI = null;
    private List<string> m_dialogLines = new();
    private int m_dialogLineIndex = 0;
    //private bool m_useAltLines = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneChanged;

        m_textUI = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneChanged;
    }


    private void OnSceneChanged(Scene _scene, LoadSceneMode _loadSceneMode)
    {
        bool isLevel1 = (_scene.name.Contains("1"));
        bool isLevel2 = (_scene.name.Contains("2"));
        bool isLevel3 = (_scene.name.Contains("3"));

        switch (isLevel1)
        {
            case true:
                {
                    UseLevel1Lines();
                    
                }
                break;
            case false:
                {
                    switch (isLevel2)
                    {
                        case true:
                            {
                                UseLevel2Lines();
                                
                            }
                            break;
                        case false:
                            {
                                switch (isLevel3)
                                {
                                    case true:
                                        {
                                            UseLevel3Lines();
                                            
                                        }
                                        break;
                                    case false:
                                        {
                                            
                                        }
                                        break;
                                    default:
                                        {
                                            LogUtils.LogError("WTF lvl3");
                                            
                                        }
                                        break;
                                }
                                break;
                            }
                        default:
                            {
                                LogUtils.LogError("WTF lvl2");
                                
                            }
                            break;
                    }
                }
                break;
            default:
                {
                    LogUtils.LogError("WTF lvl1");
                    
                }
                break;
        }

        SayNextLine();
    }

    //public void UseAltLines()
    //{
    //    m_useAltLines = true;
    //}

    public void SayNextLine()
    {
        if (m_dialogLineIndex + 1 >= m_dialogLines.Count)
        {
            return;
        }

        m_textUI.text = m_dialogLines[++m_dialogLineIndex];
    }
    public void SayPreviousLine()
    {
        if (m_dialogLineIndex - 1 < 0)
        {
            return;
        }

        m_textUI.text = m_dialogLines[--m_dialogLineIndex];
    }

    public void SayThisNow(string _this)
    {
        m_textUI.text = _this; // Now
    }


    private void ClearLinesList()
    {
        m_dialogLines.Clear();
        m_dialogLineIndex = 0;
    }
    public void AddLine(string _line)
    {
        m_dialogLines.Add(_line);
    }
    public void RemoveLine(string _line)
    {
        if (!m_dialogLines.Contains(_line))
        {
            LogUtils.LogInfo("u dyslexic or what ?");
            return;
        }

        m_dialogLines.Remove(_line);
    }
    public void RemoveLine(int _index)
    {
        if (_index < 0 || _index >= m_dialogLines.Count)
        {
            LogUtils.LogWarning("Index out of range...");
            return;
        }

        m_dialogLines.RemoveAt(_index);
    }
    private void UseLevel1Lines()
    {
        ClearLinesList();

        // Player
        _ = AddAndSayLater("Hey Grandma ! Where are you ?", Random.Range(3f, 7f));

        // Granny
        _ = AddAndSayLater("mhdguqscfgh (I'm buried under here)", Random.Range(3f, 7f));

        // Player
        _ = AddAndSayLater("Ah, yes, u ded, mb i forgor", Random.Range(3f, 7f));
        _ = AddAndSayLater("*sobs*", Random.Range(3f, 7f));

        // Granny
        _ = AddAndSayLater("No problem, my dear. Are you sad to see me dead ?", Random.Range(3f, 7f));

        // Player
        _ = AddAndSayLater("*sobs* Yes... I'd want you to undead yourself", Random.Range(3f, 7f));

        // Granny
        _ = AddAndSayLater("Haha! So I will, and I'll bake you some cookies !", Random.Range(3f, 7f));

        // Player
        _ = AddAndSayLater("*happiness noises*", Random.Range(3f, 7f));

        // Granny
        _ = AddAndSayLater("Collect 10 coins to help me, granny needs money for cookies", Random.Range(3f, 7f));

        // Player
        _ = AddAndSayLater("I'm on it !", Random.Range(3f, 7f));
        _ = AddAndSayLater("Here they are, grandma !", Random.Range(3f, 7f));

        // Granny
        _ = AddAndSayLater("Ooohhh how sweet of you, thank you my dear !", Random.Range(3f, 7f));
        _ = AddAndSayLater("Prepare your stomach, but first we have something else to attend to...", Random.Range(3f, 7f));
    }
    public async Task SayThisLater(string _this, float _laterInSeconds)
    {
        await Task.Delay(Mathf.CeilToInt(1000f * _laterInSeconds));

        SayThisNow(_this);
    }
    private async Task AddAndSayLater(string _line, float _laterInSeconds)
    {
        await Task.Delay(Mathf.CeilToInt(1000f * _laterInSeconds));

        m_dialogLines.Add(_line);
        SayThisNow(_line);
    }

    private void UseLevel2Lines()
    {
        ClearLinesList();

        // Player
        m_dialogLines.Add("");
    }
    private void UseLevel3Lines()
    {
        ClearLinesList();


    }
}
