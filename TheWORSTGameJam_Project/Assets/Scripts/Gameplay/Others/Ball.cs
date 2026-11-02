using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI.Table;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 120f)]
     private float m_timeToLive = 10f;

    private MeshRenderer m_mr = null;
    private float m_ttlTimer = 0f;
    float laodTimer = 10f;
    private void Awake()
    {
        m_mr = GetComponent<MeshRenderer>();
        Material mat = m_mr.material;
        mat.color = ColourUtils.RandomRGB01();
        m_mr.material = mat;
    }

    private void Update()
    {
        m_ttlTimer += Time.deltaTime;

        if (m_ttlTimer >= m_timeToLive)
        {
            Destroy(gameObject);
        }
        if (laodTimer > 0f)
        {
            laodTimer -= Time.deltaTime;
        }
        else
            SceneManager.LoadScene("Level3");
    }
    [SerializeField] private AudioClip hastenLoadingSound = null;
    public void OnHastenLevel3Loading(InputAction.CallbackContext c_ontext)
    {
        if (c_ontext.started)
        {
            laodTimer -= 0.2f;
            AudioManager.s_Instance.Play2D(hastenLoadingSound, new AudioManager.AudioParams
                (
                1f, Random.Range(0.05f, 1000f),
    (Random.Range(0, 10) == 0) ? true : false
                )
                );

            string[] texts = {
                "Quick, Level3 !",
                "Hayaku!",
                "Hayaku, hayaku!",
                "Oh dépêche toi !",
                "Tu va le bouger ton cul oui ?!",
                "*sigh* Can you PLEASE load faster ?",
                "MAMMA MIA!",
                "Fast & Furi-[COPYRIGHT]",
                "Allez, allez !",
                "Ion wanna wait.",
                "I don't have all day",
                "My life, it's potatoes",
                "The cake is a lie",
                "The wait is (not) a lie however",
                "Granny's happy, and so are you",
                "Cookies, cookies !!!",
                ":D",
                "Fat-free, protein-free, but a whole lot of sugar",
                "Respect nature, dammit !",
                "It's getting long...",
                "FUCKING.LOAD.FASTER!!!",
                "I'll wait",
                "I am calm",
                "Calm and tranquill",
                "Call me a monk if you want",
                "Imma tire",
                "MAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                "<< Forgive me Father,"+
"My Lord, I will commit a sin..."+

"I will not drink, I will not be tempted by the sin of flesh;"+
            "but these horrors have awoken..."+

"A false god tries to substitute your greatness"+

"The time of prayers is done,"+
"Now it's time to barter Holy book with real weapons"+

"My sin is empowered by my will to protect humanity"+
"And to show them who's"+
"the real God >> ",
                "I was a priest, once",
                "Welcome to Liberty City",
                "Hey, Nico! Wanna go bowling?",
                "Hey, listen !",
                "Well excuuuuuuuuuuuuuuuuuuuuuuuuuse me, princess",
                "THE WAITING IS LITTLE LONGER THAN WHAT I CAN FATHOM",
                "Urghhh",
                "HURRY FOR GOODNESS SAKE, HURRRRRYYYYYYYYYYYYYY",
                "Pretty please ?",
                "I love you - English",
                "Je t'aime - Français",
                "Te quiero - Español",
                "Eu amo-te - Português",
                "Something else - Some other language",
                "Macron *putain* EXPLOSION",
                "Bismillah c'est les tenèbres",
                "1+1=2",
                "2+2=4",
                "4+4=8",
                "8+8=16",
                "16+16=32",
                "32+32=64",
                "64+64=128",
                "128+128=256",
                "256+256=512",
                "512+512=1024",
                "1024+1024=agoodpuzzlegame",
                "agoodpuzzlegame+agoodpuzzlegame=4096",
                "aussi simple que 2 et 2 font 4, salukes",
                "Eu gosto de mamar\nOs peitos da cabritinha",
                "O garagem é usado mas teu carro vai gostar",
                "Foda-se esta merda, é demoRAAAAAAAAAAAAAAAAAADO",
                "deMoRaDOOOOOOOOOOOOOOOOOOOOO",
                "toO sLOOOOOOOOOOOOOOOOOOW",
                "troP LoNNNNNNNNNNNNNNNNNNNGGGGGGG",
                "Faster is faster",
                "\"Y a pas de petites économies\"",
                "We're on Easy Stree\nAnd it feels so sweet",
                "Little pig, little pig\nLet me in !",
                "Pizzas are circles that we cut in triangles to then store in square boxes",
                "Math is beautiful",
                "Love if beautiful",
                "Life is beautiful",
                "This game is NOT beautiful",
                "Caca",
                "Bière, Beer, Cerveja",
                "I can't wait any longer",
                "Alt + F4",
                "Sinon, ça va vous ?",
                "The weather's good today",
                "Kaaaaaa",
                "Meeeeee",
                "Haaaaaa",
                "HAAAAAA !",
                "KaaameeehaaameeeeeeHAAAAAAAAA !",
                "5 minutes = roughly 6 hours",
                "48min ? You mean a couple dozens of episodes right ?",
                "Level1, check! Level2, check! Level3... STILL WAITING !",
                "*crying*",
                "Almost there",
                "Almost there.",
                "Almost there..",
                "Almost there...",
                "Almost there..!",
                "Almost there..?",
                "There! Wait, nevermind",
                "Aaaaand there we go !"
            };

            LogUtils.LogInfo(texts[Random.Range(0, texts.Length)], Color.magenta);
        }
    }
    public void Throw(Vector3 _throwForce)
    {
        GetComponent<Rigidbody>().AddForce(_throwForce, ForceMode.Impulse);
    }
}
