using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public static class RandVector2Caralho
{
    public static void Randomise(this Vector2 float2, float _factor = 1f)
    {
        float2.x = _factor * Random.Range(-1f, 1f);
        float2.y = _factor * Random.Range(-1f, 1f);
    }
}
public class MenuButton : UIThing
{
    public static bool TheChoiceHasBeenMade = false;
    private enum ButtonType
    {
        PLAY,
        QUIT,

        RESPECT,

        RESTART,
        QUIT2,

        CRASH,

        ELSE
    }

    [SerializeField] private ButtonType m_type = ButtonType.PLAY;
    [SerializeField]
    [Min(0f)]
    private float m_scaleChangeSpeed = 2f;

    private RectTransform m_rectTransform = null;
    private float m_scaleChangeTimer = 0f;
    private float m_scaleChangeOffset = 1f;
    Vector2 movement = default;
    private void Awake()
    {
        movement.Randomise(Random.Range(0.5f, 10f));
        m_rectTransform = GetComponent<RectTransform>();

        GetComponent<Button>().onClick.AddListener(OnClick);

        float offsetValue = 0.25f;
        m_scaleChangeOffset += Random.Range(-offsetValue, offsetValue);

        string typeAsStr =
            m_type
            .ToString()
            .ToLower()
            .FirstCharacterToUpper();
        string pathStart = $"Buttons/{typeAsStr}Button";

        FetchSound(pathStart + "_Hover");
        FetchSound(pathStart + "_Interact");
    }

    private void Update()
    {
        m_scaleChangeTimer += Time.deltaTime * m_scaleChangeSpeed;

        float newScaleValue = m_scaleChangeOffset * m_scaleChangeTimer;
        float newScaleX = 1f + 0.5f * Mathf.Sin(newScaleValue);
        float newScaleY = 1f + 0.5f * Mathf.Cos(newScaleValue);
        Vector3 localScale = m_rectTransform.localScale;
        localScale.x = newScaleX;
        localScale.y = newScaleY;
        m_rectTransform.localScale = localScale;

        if (m_scaleChangeTimer > 1000000f)
        {
            m_scaleChangeTimer = 0f;
        }
        GetComponent<RectTransform>().Translate(Time.deltaTime * movement);
        transform.Translate(Time.deltaTime * movement);
    }


    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    private void QuitGame()
    {
        Application.Quit();
    }
    private void PayRespects()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            LogUtils.LogWarning("Wtf");
            return;
        }

        player.PayRespects();

        Granny baba = FindAnyObjectByType<Granny>();
        if (baba == null)
        {
            LogUtils.LogWarning("Where is granny ?");
            return;
        }

        baba.AppearAsGhost();
        Destroy(gameObject);
    }
    private void Restart()
    {
        MenuButton.TheChoiceHasBeenMade = true;
        StartGame();
    }
    private void Quit2()
    {
        MenuButton.TheChoiceHasBeenMade = true;
        QuitGame();
    }
    private void Crash()
    {
        FindAnyObjectByType<TextBox>().SayThisNow("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        QuitGame();
    }
    private void OnClick()
    {
        switch (m_type)
        {
            case ButtonType.PLAY: StartGame(); break;
            case ButtonType.QUIT: QuitGame(); break;
            case ButtonType.RESPECT: PayRespects(); break;
            case ButtonType.RESTART: Restart(); break;
            case ButtonType.QUIT2: Quit2(); break;
            case ButtonType.CRASH: Crash(); break;
            case ButtonType.ELSE: SayRandomStuff(); break;
            default: break;
        }
    }


    private void SayRandomStuff()
    {
        string[] stuff =
        {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",

            "a",
            "b",
            "c",
            "d",
            "e",
            "f",
            "g",
            "h",
            "i",
            "j",
            "k",
            "l",
            "m",
            "n",
            "o",
            "p",
            "q",
            "r",
            "s",
            "t",
            "u",
            "v",
            "w",
            "x",
            "y",
            "z",
            "abcdefghijklmnopqrstuvwxyz",

            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ",

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
            "<< Forgive me Father," +
"My Lord, I will commit a sin..." +

"I will not drink, I will not be tempted by the sin of flesh;" +
            "but these horrors have awoken..." +

"A false god tries to substitute your greatness" +

"The time of prayers is done," +
"Now it's time to barter Holy book with real weapons" +

"My sin is empowered by my will to protect humanity" +
"And to show them who's" +
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
            "Aaaaand there we go !",

            "Cha-la, head cha-la",
            "Nanani nanana",
            "wtf",
            "Wtf",
            "WtF",
            "wTf",
            "WTF",
            "Apple",
            "Pear",
            "Banana",
            "Apricot",
            "Pomme tomate rouge",
            "POPO POLOP..!",
            "Fly like a butterfly, sting like a bee",
            "Be like water",
            "Ya like jazz?",

            "White",
            "Red",
            "Green",
            "Blue",
            "Cyan",
            "Yellow",
            "Magenta",
            "Black",

            "Light gray",
            "Gray",
            "Dark gray",
            "Light grey",
            "Grey",
            "Dark grey",
            "So many shades of gray",
            "So many shades of grey",

            "Brown",
            "Maroon",
            "Teal",
            "Aqua",
            "Emerald",
            "Pink",
            "Violet",
            "Purple",

            "L'équipe est toka",
            "Ish ish",
            "Le J c'est le S",
            "Lourd comme une plume",
            "Voix de miel",
            "Shemenemene shimiyay",

            "Stay calm",
            "Everything's good?",
            "Everything's alright?",
            "Alright",
            "All right",
            "Nice",
            "Good",
            "Pretty good",
            "Pretty",
            "Cute",
            "Like",
            "Appreciate",
            "Love",

            "Sqrt(25) = 5",
            "5^5 = 25",

            "What is 1^2 ?",
            "What is 2^2 ?",
            "What is 3^2 ?",
            "What is 4^2 ?",
            "What is 5^2 ?",
            "What is 6^2 ?",
            "What is 7^2 ?",
            "What is 8^2 ?",
            "What is 9^2 ?",

            "What is 1^3 ?",
            "What is 2^3 ?",
            "What is 3^3 ?",
            "What is 4^3 ?",
            "What is 5^3 ?",
            "What is 6^3 ?",
            "What is 7^3 ?",
            "What is 8^3 ?",
            "What is 9^3 ?",

            "What is 4! ?",
            "Factorial is when you multiply 1 by 2 by 3 by ... by n, with n bein the number you want to factor",
            "Quaternions sure are practical",
            "Euler angles",
            "Verlet integration",
            "Bézier curves",
            "Factorial",
            "Squared",
            "Cubed",
            "Added",
            "Subtracted",
            "Multiplied",
            "Divided",
            "Modulo",
            "Percentage",
            "Quaternion",
            "Octonion",
            "Even number",
            "Odd number",
            "Imaginary number",
            "Complex number",
            "Vector calculations",

            "whole number",
            "integer number",
            "integer",
            "int",
            "unsigned",
            "unsigned int",
            "uint",
            "floating-point number",
            "floating-point",
            "float",
            "double",
            "decimal",
            "char",
            "string",
            "char array",
            "char pointer",
            "pointer",
            "ptr",
            "Function",
            "Method",
            "Algorithm",
            "Monad",
            "Sequence",

            "To be or not to be",
            "That's the question",
            "To be or not to be, that's the question",
            "To be is to be",
            "Do be do be do",

            "Lol",
            "Lolol",
            "Lololol",
            "Trololo",
            "Laughing out loud",
            "Lmao",
            "Lmfao",
            "*laughing*",

            "Smile, Sweet, Sister, Sadistic, Surprise, Service, we are Style !",
            "Smile, Sweet, Sister, Sadistic, Surprise mothafocka",
            "Oh love me mister",
            "Daddy daddy do",
            "JOOOOOOOOOOOOOOOOOOOOOOOOOJO",
            "Jojo",
            "JoJo",
            "Goodbye, JoJo!",
            "WRYYYYYYYYYYY",
            "Hinjaku hinjaku !",
            "Wryryryryryryyy !",
            "B-akana!",
            "S-H-I-T",
            "S-H-I-T, SHIIIIIIT",
            "Crabpaskets",

            "This game is bad, isn't it ?",
            "This is pure crap, wtf",
            "Well, some things could be worse",
            "At least there's no epilepsy nor loud sound",
            "Crap",
            "Come here !",
            "Gimme a huuug",
            "*pats ur lil head*",

            "Lucille is veery thirsty tonight",

            "Keyboard",
            "Mouse",
            "Gamepad",
            "Controller",
            "Joystick",
            "Remote",

            "Hello, monkeys !",
            "*evil laugh*",
            "Shurololololo",
            "Zeehahahahahaaa",
            "Freedom is over there",
            "Mange mon pain Eren",
            "Akh nein !",
            "Frauleine",
            "Der Apfel ist rot",
            "Ein",
            "Swei",
            "[Three in German]",
            "[Four in German]",
            "[Five in German]",
            "[Six in German]",
            "[Seven in German]",
            "[Eight in German]",
            "[Nine in German]",
            "If 1 is 1, but 1 isn't 1, the what is 1 ?",
            "I think it's 1",

            "1+2+3+4+5+6+7+8+9 = ?",

            "Six Seven",
            "6 7",

            "The ravioli is a lie",
            "The cake is alie... or is it ?",
            "Beep boop",
            "This video is sponsored by [insert sponsor here]",
            "Salut, c'est Franck Leboeuf",
            "Cargloush ripare, Cargloush roumplash",
            "Water over lava equals obsidian",
            "Lava over water equals stone",
            "Both confronted give cobblestone",
            "The Overworld",
            "The Nether",
            "The End",
            "The Sift",
            "The Aether",
            "I am Steve",
            "Meow",
            "Meow meow",
            "Meow meow (I'm hungry)",
            "You give me food? You must be a god! (dog's perspective)",
            "You give me food? I must be a god! (cat's perspective)",
            "Bark, or not, why would you even do that ?",
            "Does anyone even read this ?",
            "Hello",
            "Hi",
            "Bonjour",
            "Holà",
            "Olà",
            "Buon giorno",
            "Guten tag",
            "I love sweet potatoes so much",
            "Lucia would never say this",
            "Mashed potatoes with garlic and meeeeaaaattttttt",
            "I'm hungry",
            "I'm thirsty",
            "I like trains",
            "Whoosh",
            "*whoosh*",
            "r/whoosh",
            "r/degoogle",
            "r/fossdroid",
            "r/learnprogramming",
            "r/csfml",
            "Wow, I'm super strong !",
            "Wow, you're super strong !",
            "Strength",
            "Force",
            "Stamina",
            "Endurance",
            "Power",
            "Spaghetti",
            "Running",
            "Running.",
            "Running..",
            "Running...",
            "Running...",
            "Waiting",
            "Waiting.",
            "Waiting..",
            "Waiting...",
            "Waiting...",
            "Button",
            "You clicked on a button, are you happy ?",
            "The button is happy",
            "The button is sad",
            "The button is angry",
            "The button is hungry",
            "The button is thirsty",
            "The button is a button",
            "The button is clicked",
            "The button is perplexed",
            "The button is thinking",
            "The button is",
            "The button is overjoyed",
            "The button is depressed",
            "The button is a lie",
            "The button is indeed not a cake",
            "The button is not a slider",
            "The button is the button",
            "Sleep",
            "Go to sleep",
            "Go to bed",
            "*yawmning*",
            "*sleeping*",
            "I'm so full of energy !",
            "Feel good",
            "Feels good",
            "I like that !",
            "I love that !",
            "I appreciate that !",
            "I enjoy that !",
            "Hey, it's me !",
            "Ouch!",
            "Continue",
            "Please continue",
            "Do it again",
            "Do it again.",
            "Again",
            "Again.",
            "No more",
            "Not now",
            "Later",
            "Maybe later",
            "Afterward",
            "Afterwards",
            "Now",
            "NOW",
            "Thank you",
            "Thanks",
            "Merci",
            "Gracias",
            "Obrigado",
            "Obrigada",
            "Obarigado/a",
            "Arigato",
            "Xexe",
            "You are important",
            "Think about it",
            "Consider it",
            "Go for it",
            "Believe in yourself",
            "Take care"
        };

        FindAnyObjectByType
            <TextBox>
            ()
            .SayThisNow
            (stuff
             [Random
              .Range
                (0, stuff.Length)
            ]
        );
    }
}
