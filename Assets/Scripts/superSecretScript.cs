using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class superSecretScript : MonoBehaviour
{

    //public Sprite playerSprite;
    public GameObject player;
    public Sprite spiritSprite;
    public Sprite heroSprite;
    public GameObject necromancer;
    public bool cutsceneStarted = false;
    public bool finishedFade = false;
    public bool startedApproach = false;
    public bool atPosition = false;
    public Camera mainCamera;
    public Transform moveToTarget;
    public float distanceBetweenPlayer;
    public GameObject FinalSpeechPanel;
    public GameObject Canvas;
    public GameObject ChoiceButtonsPanel;
    public GameObject narratorPanelEvil;
    public GameObject narratorPanelHero;
    public GameObject narratorPanelBud;
    public GameObject finalButtonPanel;
    public Text narratorTextHero;
    public Text narratorTextEvil;
    public Text narratorTextBud;
    public GameObject canvasPanel;
    public Text textBox;
    public float timeLapse = .1f;
    public bool doneTalking;
    private bool heroTime = false;
    private bool isTyping = false;
    private bool presentedOptions = false;

    [Header("Text Stuffs")]
    public string finalSpeech = "Ah.. My hubris.... Come little one... Join me... and I shall grant you.... your old life.... and together... we shall rule..... all....";

    public Animator anim;

    public AudioSource source;
    public AudioSource musicSource;
    public AudioClip deathScream;
    public AudioClip possessSound;
    public AudioClip evilLaughSound;
    public AudioClip transformSound;
    public AudioClip heroMusic;
    public AudioClip evilMusic;
    public AudioClip budMusic;
    public AudioClip ambientMusic;

    private void Awake()
    {
        source = this.GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {

        //player.GetComponent<SpriteRenderer>().sprite = GameObject.FindGameObjectWithTag("Possessed").GetComponent<SpriteRenderer>().sprite;
        //Destroy(GameObject.FindGameObjectWithTag("Possessed"));
        Canvas.SetActive(false);
        FinalSpeechPanel.SetActive(false);
        ChoiceButtonsPanel.SetActive(false);
        narratorPanelHero.SetActive(false);
        narratorPanelBud.SetActive(false);
        narratorPanelEvil.SetActive(false);
        finalButtonPanel.SetActive(false);
        anim = player.GetComponent<Animator>();
        musicSource.PlayOneShot(ambientMusic);
        //SceneManager.UnloadSceneAsync(6);
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenPlayer = Vector3.Distance(player.gameObject.transform.position, moveToTarget.position);

        if (!cutsceneStarted)
        StartCutscene();


        if (finishedFade && !startedApproach)
        {
            
            approachBoss();
        }
        if(startedApproach && distanceBetweenPlayer > 2f)
        {
           
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, moveToTarget.position , 3f * Time.deltaTime);
        }
        else if(distanceBetweenPlayer <= 2f && !atPosition)
        {
            atPosition = true;
            StartCoroutine(zoomOutCamera(2f));
        }
        if(atPosition && !isTyping)
        {
            Canvas.SetActive(true);
            FinalSpeechPanel.SetActive(true);
            StartCoroutine(BuildText(textBox, finalSpeech));
        }
        if(doneTalking && !presentedOptions)
        {
            presentedOptions = true;
            FinalSpeechPanel.SetActive(false);
            ChoiceButtonsPanel.SetActive(true);
        }
    }

   private void StartCutscene()
    {
        cutsceneStarted = false;
        // StartCoroutine(fadeOut(player.GetComponent<SpriteRenderer>(), 2f, spiritSprite));
        finishedFade = true;
    }


    IEnumerator fadeOut(SpriteRenderer MyRenderer, float duration, Sprite newSprite) //death animation, prevents player from moving while dying as well
    {
        player.GetComponent<Animator>().SetTrigger("changeToHero");
        yield return new WaitForSeconds(2f);
    }

    IEnumerator fadeOut(SpriteRenderer MyRenderer, float duration) //death animation, prevents player from moving while dying as well
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;
        // this.gameObject.GetComponent<BasicMovement>().enabled = false;
        //this.gameObject.GetComponent<BasicAI>().enabled = false;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            //Debug.Log(alpha);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
 
    }

    IEnumerator fadeIn(Image MyRenderer, float duration)
    {
        float counter = 0;
        Color spriteColor = MyRenderer.material.color;
        while(counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, counter / duration);
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
    }

    IEnumerator fadeOut(Animator animator)
    {
        animator.SetTrigger("Fade");
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
        mainCamera.GetComponent<Animator>().SetTrigger("pan");
        yield return new WaitForSeconds(1f);
        mainCamera.transform.position = new Vector3(necromancer.transform.position.x, necromancer.transform.position.y, -10);
        mainCamera.transform.parent = necromancer.transform;
        Destroy(mainCamera.GetComponent<Animator>());
        Destroy(player);
    }

    IEnumerator zoomOutCamera(float duration)
    {
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float cameraSize = Mathf.Lerp(mainCamera.orthographicSize, 8, counter / duration);
            mainCamera.orthographicSize = cameraSize;
            yield return null;
        }
    }


    private void approachBoss()
    {
        startedApproach = true;
        mainCamera.transform.parent = player.transform;

    }

    private IEnumerator BuildText(Text textBox, string message)
    {
        textBox.text = "";
        isTyping = true;
        for (int i = 0; i < message.Length; i++)
        {
            textBox.text = string.Concat(textBox.text, message[i]);
            //Wait a certain amount of time, then continue with the for loop
            yield return new WaitForSeconds(timeLapse);
        }
        yield return new WaitForSeconds(3f);
        doneTalking = true;

    }

    private IEnumerator WaitForFadeInHero(float duration)
    {
        yield return new WaitForSeconds(duration);
        canvasPanel.SetActive(true);
        canvasPanel.GetComponent<Animator>().SetTrigger("fade");
        narratorPanelHero.SetActive(true);
        StartCoroutine(BuildText(narratorTextHero, narratorTextHero.text));
        finalButtonPanel.SetActive(true);
    }

    private IEnumerator WaitForFadeInEvil (float duration)
    {
        yield return new WaitForSeconds(duration);
        canvasPanel.SetActive(true);
        canvasPanel.GetComponent<Animator>().SetTrigger("fade");
        narratorPanelEvil.SetActive(true);
        StartCoroutine(BuildText(narratorTextEvil, narratorTextEvil.text));
        finalButtonPanel.SetActive(true);
    }

    private IEnumerator WaitForFadeInBud(float duration)
    {
        yield return new WaitForSeconds(duration);
        canvasPanel.SetActive(true);
        canvasPanel.GetComponent<Animator>().SetTrigger("fade");
        narratorPanelBud.SetActive(true);
        StartCoroutine(BuildText(narratorTextBud, narratorTextBud.text));
        finalButtonPanel.SetActive(true);
    }


    private IEnumerator WaitToLaugh(float duration)
    {
        yield return new WaitForSeconds(duration);
        source.PlayOneShot(evilLaughSound);
        StartCoroutine(WaitForFadeInEvil(2f));
    }

    public void heroEnd()
    {
        musicSource.Stop();
        musicSource.PlayOneShot(heroMusic);
        ChoiceButtonsPanel.SetActive(false);
        source.PlayOneShot(deathScream);
        StartCoroutine(fadeOut(necromancer.GetComponent<SpriteRenderer>(), 2f));
        StartCoroutine(WaitForFadeInHero(2f));
    }

    public void evilEnd()
    {
        musicSource.Stop();
        musicSource.PlayOneShot(evilMusic);
        ChoiceButtonsPanel.SetActive(false);
        source.PlayOneShot(possessSound);
        StartCoroutine(fadeOut(player.GetComponent<Animator>()));
        StartCoroutine(WaitToLaugh(2f));
    }

    public void budEnd()
    {
        musicSource.Stop();
        musicSource.PlayOneShot(budMusic);
        ChoiceButtonsPanel.SetActive(false);
        source.PlayOneShot(transformSound);
        StartCoroutine(fadeOut(player.GetComponent<SpriteRenderer>(), 2f, heroSprite));
        heroTime = true;
        StartCoroutine(WaitForFadeInBud(5f));

    }


}
