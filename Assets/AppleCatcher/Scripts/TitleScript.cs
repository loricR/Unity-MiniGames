using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{

    //Music attributs
    public AudioClip musique;
    public AudioClip validate;
    private AudioSource ref_audioSource_music;
    private AudioSource ref_audioSource_validate;

    //Transition effect 
    public GameObject WhiteSquare;
    private float speedSquare = 0.1f;
    private SpriteRenderer spriteren;

    private bool transition_processing = false;

    // Start is called before the first frame update
    void Start()
    {
        //Getting access to the sprite renderer of the white square
        spriteren = WhiteSquare.GetComponent<SpriteRenderer>();

        //Setting the sounds ready and playing the music
        AudioSource music = gameObject.AddComponent<AudioSource>();
        ref_audioSource_music = music;
        music.loop = true;
        music.clip = musique;
        music.Play();

        AudioSource valid = gameObject.AddComponent<AudioSource>();
        ref_audioSource_validate = valid;
        valid.loop = false;
        valid.clip = validate;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !transition_processing)
        {
            transition_processing = true;
            ref_audioSource_music.Stop();
            ref_audioSource_validate.Play();

            StartCoroutine(ChangeSquare());
            StartCoroutine(LoadScene_Game());
        }
    }

    IEnumerator LoadScene_Game()
    {
        yield return new WaitUntil(() => spriteren.color.a >= 1);
        yield return new WaitUntil(() => ref_audioSource_validate.isPlaying == false);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

        //Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator ChangeSquare()
    {
        float alpha = 0;
        while(alpha < 1)
        {
            alpha = alpha + 0.1f;
            Vector4 vect = new Vector4(255, 255, 255, alpha);
            spriteren.color = vect;
            yield return new WaitForSeconds(speedSquare);
        }
        Debug.Log("Transition done");
    }

}
