using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public Text txt;
	public Button backBut;
	
	public GameObject[] PrefabObjects;
	
	private GameObject oneBalka;
	
	private Rigidbody playerRB;
	private AudioSource audio;
		
	private float sensivity = 100f;
	private float loudness = 0f;
	private float maxY = 10f;
	private int tick, enemyCounter;
	
	private int enemyDelay = 200;

	private void Start(){
		backBut.onClick.AddListener(OnBackBut);
		tick = 0;
		enemyCounter = 0;
		STATIC.putEnemyOnTop = false;
		STATIC.isDead = false;
		STATIC.LEVEL = 0;
		playerRB = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
		StartCoroutine(StartMic());
	}
	

	IEnumerator StartMic(){
		yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
		if(Application.HasUserAuthorization(UserAuthorization.Microphone))
		{
			audio.clip = Microphone.Start(null, true, 1, 44100);
			audio.loop = true;
			audio.mute = false;
			while (!(Microphone.GetPosition(null) > 0)){ }
			audio.Play();
		}
		else {
			OnBackBut();
		}
	}

	private void Update(){
		if (!STATIC.isDead) {
			txt.text = STATIC.SCORE_TEXT + STATIC.DISTANCE;
			loudness = GetAverageVolume() * sensivity;
			print(loudness);
			if (loudness > STATIC.LOUD_FOR_REACT) {
//			float newY = ((loudness - STATIC.LOUD_FOR_REACT) / 100f) * maxY;
				float newY = loudness - STATIC.LOUD_FOR_REACT;
				if (newY > 10f) {
					newY = 10f;
				}

				playerRB.velocity = new Vector3(playerRB.velocity.x, newY, playerRB.velocity.z);
			}

			Vector3 pos = transform.position;
			if (pos.y > STATIC.MAX_Y) {
				pos.y = STATIC.MAX_Y;
				transform.position = pos;
			}

			if (pos.y < STATIC.MIN_Y) {
				pos.y = STATIC.MIN_Y;
				transform.position = pos;
			}

			if ((tick % enemyDelay == 0 && enemyCounter < 10 + STATIC.LEVEL) || tick == 0) {
				enemyCounter++;
				oneBalka = Instantiate(PrefabObjects[0], new Vector3(0, 0, 0), Quaternion.identity);
			}

			tick++;
			STATIC.LEVEL = (int) (tick / 20f);
			STATIC.DISTANCE = (int) (tick / 100f);
		}
	}
	
	void OnBackBut(){
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

	float GetAverageVolume(){
		float[] data = new float[256];
		float a = 0;
		audio.GetOutputData(data, 0);
		foreach (float s in data) {
			a += Mathf.Abs(s);
		}

		return a / 256f;
	}
	
	
	
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag.Equals(STATIC.TAG_ENEMY) && !STATIC.isDead) {
			playerRB.useGravity = false;
			playerRB.velocity = new Vector3(0f, 0f, 0f);
			STATIC.isDead = true;
			StartCoroutine(End());
		}
		
	}

	IEnumerator End(){
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("End", LoadSceneMode.Single);
	}
}
