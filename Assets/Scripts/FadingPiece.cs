using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPiece : MonoBehaviour {

    public transformation tr;
    public int numberToStartAnimation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (numberToStartAnimation == 1)
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
        tr.pieceFinishedFading();
    }

    public void finishReverseFading()
    {

    }
}
