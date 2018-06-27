using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject[] objs;
	private int[] objRange = {10, 20, 30, 40, 50, 60};
	private Vector3 pos;
	private float step = 0.1f;
	
	void Start () {
		OnStart();
	}
	
	void Update () {
		if (!STATIC.isDead) {
			pos = transform.localPosition;
			pos.x -= step;
			transform.localPosition = pos;

			if (pos.x < -STATIC.ENEMY_START_X) {
				OnStart();
			}
		}
	}

	private void OnStart(){
		
		int rand = Random.Range(0, STATIC.LEVEL);
		int maxObj = 0;
        
		for (int i = 0; i < objs.Length; i++)
		{
			objs[i].SetActive(false);
			if (rand > objRange[i])
			{
				maxObj = i + 1;
			}
		}

		rand = Random.Range(0, maxObj);
		objs[rand].SetActive(true);
		
		transform.localPosition = new Vector3(STATIC.ENEMY_START_X, 0f, 0f);
	}
}
