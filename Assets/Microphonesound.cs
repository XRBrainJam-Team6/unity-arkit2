using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Microphonesound : MonoBehaviour
{

    public AudioSource audio;
    public GameObject mouth;

    private float sensivity = 100f;
    private float loudness = 0f;
    private float maxY = 10f;
    private int tick, enemyCounter;

    private int enemyDelay = 200;

    private void Start()
    {
        tick = 0;
        audio = GetComponent<AudioSource>();
        StartCoroutine(StartMic());
    }


    IEnumerator StartMic()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
        if (Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            audio.clip = Microphone.Start(null, true, 1, 44100);
            audio.loop = true;
            audio.mute = false;
            while (!(Microphone.GetPosition(null) > 0)) { }
            audio.Play();
        }
    }

    private void Update()
    {
        loudness = GetAverageVolume() * sensivity;
        //print(loudness);
        if (loudness > STATIC.LOUD_FOR_REACT)
        {
            //          float newY = ((loudness - STATIC.LOUD_FOR_REACT) / 100f) * maxY;
            float newY = loudness - STATIC.LOUD_FOR_REACT;
            mouth.SetActive(true);
        }
        else
        {
            mouth.SetActive(false);   
        }
    }

        float GetAverageVolume()
        {
            float[] data = new float[256];
            float a = 0;
            audio.GetOutputData(data, 0);
            foreach (float s in data)
            {
                a += Mathf.Abs(s);
            }

            return a / 256f;
        }
    }
