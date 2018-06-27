using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transformation : MonoBehaviour
{
    public List<GameObject> piecesOfMask;
    public float numberOfSecondsLookingAtEyes;
    public float numberOfSecondContinous = 0;
    public bool lookingAtEyes;
    public Text counterTextUI;
    public int indexOfLastPiece;
    public ParticleSystem particleSystemWhenPieceIsGone;
    public bool fading = false;
    public bool initialFade = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //nextPieceDissapearEvent();
            startFadingHead();
            print("space key was pressed");
        }

        if (Input.GetKeyDown("space"))
        {
            //nextPieceDissapearEvent();
            //startFadingHead();

            print("space key was pressed");
        }

        if (Input.GetKeyDown("space"))
        {
            //nextPieceDissapearEvent();
            //startFadingHead();
            print("space key was pressed");
        }


        if (lookingAtEyes)
        {   
            numberOfSecondContinous = numberOfSecondContinous + Time.deltaTime;
            counterTextUI.text = numberOfSecondContinous + "";
            if (numberOfSecondContinous > 8 && initialFade == false)
            {
                startFadingHead();
                initialFade = true;
            }
        }

    }

    public void startFadingHead()
    {
        fading = true;
        if (indexOfLastPiece < piecesOfMask.Count)
        {
            activateFadeAnimation(piecesOfMask[indexOfLastPiece]);
            indexOfLastPiece++;
            particleSystemWhenPieceIsGone.Clear();
            particleSystemWhenPieceIsGone.Play();
        }
    }

    public void reverseFadingAllHead()
    {
        for (int i = 0; i < piecesOfMask.Count; i++)
        {
           // Animator anim = piecesOfMask[i].GetComponent<Animator>();
            PauseSingleAnimation(piecesOfMask[i]);
        }

    }

    public void resumePlayingInSameDirectionHead()
    {
        for (int i = 0; i < piecesOfMask.Count; i++)
        {
            if (fading)
            {
                Animator anim = piecesOfMask[i].GetComponent<Animator>();
                anim.SetFloat("Speed", 1f);
            }
            else
            {
                Animator anim = piecesOfMask[i].GetComponent<Animator>();
                anim.SetFloat("Speed", -1f);
            }
            //PauseSingleAnimation(piecesOfMask[i]);
        }
    }

    public void activateFadeAnimation(GameObject piece)
    {
        Animator anim = piece.GetComponent<Animator>();
        anim.SetFloat("Speed",1f);
        anim.SetBool("Fade", true);
        //piece.SetActive(false);
    }

    public void PauseSingleAnimation(GameObject piece)
    {
        Animator anim = piece.GetComponent<Animator>();
        anim.SetFloat("Speed", 0f);
        //anim.SetBool("Fade", true);
    }

    public void resverseFadeAnimation(GameObject piece)
    {
        //Animator anim = piece.GetComponent<Animator>();
        //anim.SetBool("fade", false);
        piece.SetActive(true);
    }
    

    public void nextPieceDissapearEvent()
    {
        if (indexOfLastPiece < piecesOfMask.Count)
        {
            activateFadeAnimation(piecesOfMask[indexOfLastPiece]);
            indexOfLastPiece++;
            particleSystemWhenPieceIsGone.Clear();
            particleSystemWhenPieceIsGone.Play();
        }
    }

    public void revertPieceDissapearEvent()
    {
        if (indexOfLastPiece > 0)
        {
            resverseFadeAnimation(piecesOfMask[indexOfLastPiece]);
            indexOfLastPiece--;
        }
    }

    public void pauseAnimations()
    {

    }

    public void pieceFinishedFading()
    {
        if (fading == true)
        {
            if (indexOfLastPiece < piecesOfMask.Count)
            {
                activateFadeAnimation(piecesOfMask[indexOfLastPiece]);
                indexOfLastPiece++;
                particleSystemWhenPieceIsGone.Clear();
                particleSystemWhenPieceIsGone.Play();
            }
        }
    }

}
