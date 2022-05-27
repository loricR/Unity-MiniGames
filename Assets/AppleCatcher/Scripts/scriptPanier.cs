using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scriptPanier : MonoBehaviour
{


    private float speed = 10.0f;
    private int score = 0;
    public TextMeshPro displayedText;
    private AudioSource son;


    // Start is called before the first frame update
    void Start()
    {
        son = GetComponent<AudioSource>();
        son.loop = false;
        son.volume = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if (gameObject.transform.position.x >= 8)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            if (gameObject.transform.position.x <= -8)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "pomme")
        {
            score++;
            Debug.Log("Current score : " + score);
            displayedText.SetText("Score : " + score);
            son.Play();
        }
    }

}