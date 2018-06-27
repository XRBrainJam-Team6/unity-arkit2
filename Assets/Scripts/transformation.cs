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

    public ParticleSystem particleSystemWhenPieceIsGone2;
    public bool fading = false;
    public bool initialFade = false;
    public bool animationPaused = false;

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

        if (Input.GetKey("up"))
        {
            //nextPieceDissapearEvent();
            //startFadingHead();
            pauseAnimations();
            print("up key was pressed");
        }

        if (Input.GetKey("left"))
        {
            //nextPieceDissapearEvent();
            //startFadingHead();
            resumePlayingInSameDirectionHead();
            print("left key was pressed");
        }

        if (Input.GetKey("down"))
        {
            //nextPieceDissapearEvent();
            //startFadingHead();
            print("down key was pressed");
        }


        if (lookingAtEyes)
        {
            numberOfSecondContinous = numberOfSecondContinous + Time.deltaTime;
            counterTextUI.text = numberOfSecondContinous + "";
            if (numberOfSecondContinous > 5 && initialFade == false)
            {
                startFadingHead();
                initialFade = true;
            }
            if (animationPaused == true)
            {
                resumePlayingInSameDirectionHead();
                animationPaused = false;
            }
        }
        else {
            if (initialFade == true && animationPaused == false)
            {
                pauseAnimations();
                animationPaused = true;
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
            particleSystemWhenPieceIsGone.time = 0;
            particleSystemWhenPieceIsGone.Play();
            particleSystemWhenPieceIsGone2.Clear();
            particleSystemWhenPieceIsGone2.time = 0;
            particleSystemWhenPieceIsGone2.Play();
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

                Debug.Log("Reanimate object " + piecesOfMask[i].name);
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

        Debug.Log("Activate object " + piece.name);
        anim.SetFloat("Speed",1f);
        anim.SetBool("Fade", true);
        //piece.SetActive(false);
    }

    public void PauseSingleAnimation(GameObject piece)
    {
        Debug.Log("Pause object "+piece.name);
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
            particleSystemWhenPieceIsGone.time = 0;
            particleSystemWhenPieceIsGone.Play();


            particleSystemWhenPieceIsGone2.Clear();
            particleSystemWhenPieceIsGone2.time = 0;
            particleSystemWhenPieceIsGone2.Play();
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
        for (int i = 0; i < piecesOfMask.Count; i++)
        {
            // Animator anim = piecesOfMask[i].GetComponent<Animator>();
            PauseSingleAnimation(piecesOfMask[i]);
        }
    }

    public void pieceFinishedFading(int index)
    {
        if (fading == true)
        {
            if (index < piecesOfMask.Count)
            {
                Debug.Log("Restart Particles");
                indexOfLastPiece = index;
                activateFadeAnimation(piecesOfMask[indexOfLastPiece]);
                particleSystemWhenPieceIsGone.Clear();
                particleSystemWhenPieceIsGone.time = 0;
                particleSystemWhenPieceIsGone.Play();


                particleSystemWhenPieceIsGone2.Clear();
                particleSystemWhenPieceIsGone2.time = 0;
                particleSystemWhenPieceIsGone2.Play();
            }
        }
    }

}
