using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour {

	public Text txt;
	public Button playBut;
	
	void Start () {
		playBut.onClick.AddListener(OnPlayBut);
		txt.text = STATIC.SCORE_TEXT + STATIC.DISTANCE;
	}

	void OnPlayBut(){
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}
}
