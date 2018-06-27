using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transformation : MonoBehaviour
{
    public List<GameObject> piecesOfMask;
    public float numberOfSecondContinous = 0;
    public float numberOfSecondLookingAway = 0;
    public bool lookingAtEyes;
    public Text counterTextUI;
    public int indexOfLastPiece;
    public ParticleSystem particleSystemWhenPieceIsGone;

    public ParticleSystem particleSystemWhenPieceIsGone2;
    public bool fading = false;
    public bool initialFade = false;
    public bool animationPaused = false;
    public bool robotFinishedVanished = false;
    public bool finalParticles = false;
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
            if (robotFinishedVanished == true)
            {
                if (finalParticles == false)
                {
                    finalParticles = true;
                    var main = particleSystemWhenPieceIsGone.main;
                    main.loop = true;

                    var main2 = particleSystemWhenPieceIsGone2.main;
                    main.loop = true;
                    
                    particleSystemWhenPieceIsGone.Clear();
                    particleSystemWhenPieceIsGone.time = 0;
                    particleSystemWhenPieceIsGone.Play();
                    particleSystemWhenPieceIsGone2.Clear();
                    particleSystemWhenPieceIsGone2.time = 0;
                    particleSystemWhenPieceIsGone2.Play();
                }
            }
            else {            
                if (numberOfSecondContinous > 5 && initialFade == false)
                {
                    startFadingHead();
                    initialFade = true;
                }
                fading = true;
                if (animationPaused == true)
                {
                    resumePlayingInSameDirectionHead();
                    animationPaused = false;
                }
                Animator anim = piecesOfMask[indexOfLastPiece].GetComponent<Animator>();
                if (anim.GetBool("PieceIsFaded") == false)
                {
                    anim.SetFloat("Speed", 1f);
                }
            }
            numberOfSecondLookingAway = 0;
        }
        else {
            
            numberOfSecondLookingAway = numberOfSecondLookingAway + Time.deltaTime;
            if (numberOfSecondLookingAway > 1)
            {
                numberOfSecondContinous = 0;

                counterTextUI.text = numberOfSecondContinous + "";
            }
            if (numberOfSecondLookingAway < 3)
            {
                if (initialFade == true && animationPaused == false)
                {
                    pauseAnimations();
                    animationPaused = true;
                }

            }
            else {
                //reverse
                Animator anim = piecesOfMask[indexOfLastPiece].GetComponent<Animator>();
                if (anim.GetBool("PieceIsFaded") == false)
                {
                    anim.SetFloat("Speed", -1f);
                }
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
        fading = false;
        if (indexOfLastPiece < piecesOfMask.Count)
        {
            Animator anim = piecesOfMask[indexOfLastPiece].GetComponent<Animator>();
           /* if (anim.GetFloat == true)
            {

            }
            else
            {

            }
            */
            anim.SetFloat("Speed", -1f);
        }
        else {
            Animator anim = piecesOfMask[2].GetComponent<Animator>();
            anim.SetFloat("Speed", -1f);
        }
    }

    public void reverseIndividualPiece(GameObject piece)
    {
        Debug.Log("Pause object " + piece.name);
        Animator anim = piece.GetComponent<Animator>();
        anim.SetFloat("Speed", -1f);
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
            if (index == 3)
            {
                robotFinishedVanished = true;
            }
        }
    }

}
