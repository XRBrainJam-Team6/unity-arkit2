using UnityEngine;

public class EnemyRandomY : MonoBehaviour {

	void Start () {
		transform.localPosition = new Vector3(0f, Random.Range(STATIC.MIN_Y, STATIC.MAX_Y), 0f);
	}
}
