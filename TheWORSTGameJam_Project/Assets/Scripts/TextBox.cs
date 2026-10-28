using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TextBox : MonoBehaviour
{
    private TextMeshProUGUI m_textUI = null;
    private List<string> m_dialogLines = new();
    private int m_dialogLineIndex = 0;
    private bool m_useAltLines = false;

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

    public void UseAltLines()
    {
        m_useAltLines = true;
    }

    public void SayNextLine()
    {
        m_dialogLineIndex = Mathf.Clamp(m_dialogLineIndex + 1, 0, m_dialogLines.Count);
        m_textUI.text = m_dialogLines[m_dialogLineIndex];
    }
    public void SayPreviousLine()
    {
        m_dialogLineIndex = Mathf.Clamp(m_dialogLineIndex - 1, 0, m_dialogLines.Count);
        m_textUI.text = m_dialogLines[m_dialogLineIndex];
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

    private void UseLevel1Lines()
    {
        ClearLinesList();

        // Player
        m_dialogLines.Add("Hey Grandma ! Where are you ?");

        // Granny
        m_dialogLines.Add("mhdguqscfgh (I'm buried under here)");

        // Player
        m_dialogLines.Add("Ah, yes, u ded, mb i forgor");
        m_dialogLines.Add("*sobs*");

        // Granny
        m_dialogLines.Add("No problem, my dear. Are you sad to see me dead ?");

        // Player
        m_dialogLines.Add("*sobs* Yes... I'd want you to undead yourself");

        // Granny
        m_dialogLines.Add("Haha! So I will, and I'll bake you some cookies !");

        // Player
        m_dialogLines.Add("*happiness noises*");

        // Granny
        m_dialogLines.Add("Collect 10 coins to help me, granny needs money for cookies");

        // Player
        m_dialogLines.Add("I'm on it !");

        if (m_useAltLines)
        {
            m_dialogLines.Add("WOW! Grandma's gonna be rich with all that !");
        }
        else
        {
            m_dialogLines.Add("1!");
            m_dialogLines.Add("2!");
            m_dialogLines.Add("Three!");
            m_dialogLines.Add("Quatro!");
            m_dialogLines.Add("Cinco!");
            m_dialogLines.Add("6!");
            m_dialogLines.Add("6 7!");
            m_dialogLines.Add("EIGHT!");
            m_dialogLines.Add("NEIN!");
            m_dialogLines.Add("Aaaaannnnnnd 10 !!!");
        }

        m_dialogLines.Add("Here they are, grandma !");

        // Granny
        if (m_useAltLines)
        {
            m_dialogLines.Add("WTF did you steal that ?!");
            m_dialogLines.Add("No? Good. I'll bake tons of cookies for you");
        }
        else
        {
            m_dialogLines.Add("Ooohhh how sweet of you, thank you my dear !");
        }

        m_dialogLines.Add("Prepare your stomach, but first we have something else to attend to...");
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
