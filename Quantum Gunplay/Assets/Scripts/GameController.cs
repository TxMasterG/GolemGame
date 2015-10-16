using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public int level = 0;
	public static GameController instance = null;
	private GameObject player1;
	private GameObject player2;
	public int score1 = 0;
	public int score2 = 0;
	private  ArrayList[] clones1 = new ArrayList[5];
	private  ArrayList[] clones2 = new ArrayList[5];
	public int aliveCount;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		InitGame (level);
	
	}

	void InitGame( int round )
	{
		aliveCount = 2*(round + 1);
		player1 = (GameObject)Instantiate(Resources.Load("Player"), new Vector3(-5,-3,0), new Quaternion());
		player1.SendMessage("Initialize", 1);

		player2 = (GameObject)Instantiate(Resources.Load("Player"), new Vector3(5,3,0), new Quaternion());
		player2.SendMessage("Initialize", 2);

		player1.SendMessage("Activate");
		player2.SendMessage("Activate");

		for (int i = 0; i < round; ++i) {
			GameObject newClone1 = (GameObject)Instantiate(Resources.Load("Clone"), new Vector3 (-5, -3, 0), new Quaternion());
			newClone1.SendMessage("Initialize", 1);
			newClone1.SendMessage("Activate", clones1[i] );

			GameObject newClone2 = (GameObject)Instantiate(Resources.Load("Clone"), new Vector3 (5, 3, 0), new Quaternion());
			newClone2.SendMessage("Initialize", 2);
			newClone2.SendMessage("Activate", clones2[i] );
		}

	}

	IEnumerator PlayerDeath( int team )
	{
		if (team == 1) {
			++score2;
			clones1[level] = ((Player)player1.GetComponent("Player")).getInputs();
			player1.gameObject.SetActive(false);
			yield return new WaitForSeconds(3);
			clones2[level] = ((Player)player2.GetComponent("Player")).getInputs();
			player2.gameObject.SetActive(false);
		}
		else {
			++score1;
			clones2[level] = ((Player)player2.GetComponent("Player")).getInputs();
			player2.gameObject.SetActive(false);
			yield return new WaitForSeconds(3);
			clones1[level] = ((Player)player1.GetComponent("Player")).getInputs();
			player1.gameObject.SetActive(false);
		}
		aliveCount -= 2;
	}

	void CloneDeath( int team )
	{
		if (team == 1)
			++score2;
		else if( team == 2)
			++score1;
		--aliveCount;
	}
	
	// Update is called once per frame
	void Update () {
		if (aliveCount <= 0) {
			aliveCount = 11;
			++level;
			if( level < 5 )
				InitGame (level);
		}
	}
}
