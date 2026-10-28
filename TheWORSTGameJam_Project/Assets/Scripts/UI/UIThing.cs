using UnityEngine;
using UnityEngine.EventSystems;

public class UIThing : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private AudioClip m_hoverSound = null;
    private AudioClip m_interactionSound = null;
    private AudioManager m_audioManagerRef = null;

    private AudioManager audioManagerRef
    {
        get
        {
            if (m_audioManagerRef == null)
            {
                m_audioManagerRef = AudioManager.s_Instance;
            }

            return m_audioManagerRef;
        }
    }


    private void TryPlaySound(AudioClip _sound)
    {
        if (_sound == null)
        {
            LogUtils.LogWarning("_sound is null, you stoopid");
            return;
        }

        audioManagerRef.Play2D(_sound);
    }


    protected void FetchSound(string _path)
    {
        AudioClip loadedSound = Resources.Load<AudioClip>(_path);

        if (_path.EndsWith("_Hover"))
        {
            m_hoverSound = loadedSound;
        }
        else if (_path.EndsWith("_Click"))
        {
            m_interactionSound = loadedSound;
        }
    }


    public virtual void OnPointerEnter(PointerEventData _eventData)
    {
        TryPlaySound(m_hoverSound);
    }

    public virtual void OnPointerClick(PointerEventData _eventData)
    {
        TryPlaySound(m_interactionSound);
    }
}
