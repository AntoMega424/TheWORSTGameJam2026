using UnityEngine;
// Haha! Get rekt, typo !
using GameObejct = UnityEngine.GameObject;

[RequireComponent(typeof(Rigidbody))]
public class GameCookie : MonoBehaviour
{
    [SerializeField] private GameObejct btn1 = null;
    [SerializeField] private GameObejct btn2 = null;
    [SerializeField] private GameObejct btn3 = null;
    public static int CookieNumber = 0;
    //private static int execCount = 0;
    Rigidbody m_rb = null;
    public Vector3 direction;

    private float timeToLive = 10f;
    float btnTimmer = 0;
    private void Start()
    {
        m_rb.AddForce(Random.Range(1f, 50f) * direction, ForceMode.VelocityChange);
        btn1 = FindAnyObjectByType<Btn1>().gameObject;
        btn2 = FindAnyObjectByType<Btn2>().gameObject;
        btn3 = FindAnyObjectByType<Btn3>().gameObject;
    }
    private void Update()
    {
        if (CookieNumber > 300/*&& execCount == 0*/)
        {
            //execCount++;


            FindAnyObjectByType<TextBox>().SayThisNow("You win, you're amazing my grandchild !");
            _ = FindAnyObjectByType<TextBox>().SayThisLater("GG get rekt, bad game !", 5f);
            _ = FindAnyObjectByType<TextBox>().SayThisLater("*both laugh while smiling and looking at each other and lives happily ever after*", 7f);

            if (!MenuButton.TheChoiceHasBeenMade)
            {
                _ = FindAnyObjectByType<TextBox>().SayThisLater("Narrator: Choose. Blue = Restart, Red= Quit.", 12f);
                _ = FindAnyObjectByType<TextBox>().SayThisLater("You can only choose once.", 16f);

                btnTimmer += Time.deltaTime;

                if (btnTimmer >= 12f)
                {
                    btn1.SetActive(true);
                    btn2.SetActive(true);
                }
            }
            else
            {
                btnTimmer += Time.fixedDeltaTime;

                if (btnTimmer > 5f)
                {
                    FindAnyObjectByType<TextBox>().SayThisNow("Granny's proud of you, never forget it");
                    _ = FindAnyObjectByType<TextBox>().SayThisLater("So be proud of yourself too", 5f);
                    _ = FindAnyObjectByType<TextBox>().SayThisLater("For you have collected the coins and can drink a lot of soda", 10f);
                    _ = FindAnyObjectByType<TextBox>().SayThisLater("Which can only b done when you're alive and well, not while resting", 15f);
                    _ = FindAnyObjectByType<TextBox>().SayThisLater("So be proud, fight demons and stay alive", 20f);
                    _ = FindAnyObjectByType<TextBox>().SayThisLater("I love you, and so should you", 25f);
                    _ = FindAnyObjectByType<TextBox>().SayThisLater("*Smiles*", 30f);
                }
                if (btnTimmer > 25f)
                {
                    btn3.SetActive(true);
                }
            }
        }

        timeToLive -= Time.deltaTime;

        if (timeToLive < 0f)
        {
            Destroy(gameObject);
        }
    }
}
