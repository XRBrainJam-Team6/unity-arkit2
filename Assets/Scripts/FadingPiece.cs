using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPiece : MonoBehaviour {

    public transformation tr;
    public int numberToStartAnimation;
    public int index =0;
    public bool sentMessage = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (numberToStartAnimation == 1 && sentMessage == false)
        {
            numberToStartAnimation = 2;
            finishFading();
        }

        if (numberToStartAnimation == -1)
        {
            numberToStartAnimation = 2;
            finishReverseFading();
        }
    }

    public void finishFading()
    {
        Animator anim = this.GetComponent<Animator>();
        anim.SetBool("PieceIsFade", true);
        sentMessage = true;
        StartCoroutine(messageTimeout());
        tr.pieceFinishedFading(index);
    }

    public void finishReverseFading()
    {

    }

    

    IEnumerator messageTimeout()
    {
        yield return new WaitForSeconds(2);
        //sentMessage = false;
    }
}
