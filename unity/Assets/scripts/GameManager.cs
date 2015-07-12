using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	//variables de estado del juego

	public GameObject GAME;
	public GameObject MENU;
	public UITweener menu_final;

	public UILabel descripcion;
	public UILabel nivel_label;
	public UILabel score_label;


	public int nivel_actual;
	public GameObject[] niveles;
	public GameObject[] fichas;
	public AudioClip[] canciones_niveles;
	public AudioClip cancion_menu;

	bool menu = true;

	private int niveles_max = 3;	

	private int cont_aciertos = 0;


	// Use this for initialization
	void Start () {

		MENU.SetActive(true);
		GAME.SetActive(false);

		//cancion_menu.Play();

		audio.clip = cancion_menu;
		audio.Play();

		NotificationCenter.DefaultCenter().AddObserver(this, "match");
		//NotificationCenter.DefaultCenter().AddObserver(this, "level_clear");

		nivel_actual=0;
		niveles[nivel_actual].SetActive(true);
		fichas[nivel_actual].SetActive(true);

		for( int i = 1; i < niveles_max; i++)
		{
			niveles[i].SetActive(false);
			fichas[i].SetActive(false);

		}

		
	}


	
	// Update is called once per frame
	void Update () {
		if(nivel_actual == 0) descripcion.text = "ubica las banderas!";
		if(nivel_actual == 1) descripcion.text = "ubica los monumentos!";
		if(nivel_actual == 2) descripcion.text = "ubica las personas!";

	}

	void match(Notification noti)
	{
		int id = (int)noti.data;
		cont_aciertos++;
		score_label.text = cont_aciertos.ToString();
		Debug.Log(id.ToString());
		Debug.Log("desde noti!");
		if(cont_aciertos >= 6)
		{
			audio.Stop();
			audio.clip = canciones_niveles[nivel_actual];
			audio.Play();


			NotificationCenter.DefaultCenter().PostNotification(this, "level_clear", nivel_actual);
			cont_aciertos = 0;
			nivel_actual++;

			nivel_label.text = nivel_actual.ToString();

			Debug.Log("siguiente nivel");
			if(nivel_actual >= niveles_max)
			{
				NotificationCenter.DefaultCenter().PostNotification(this, "victory");
				Debug.Log("ganaste");
				//GAME.SetActive(false);
				menu_final.Toggle();
			}
			else 
			{
				for(int j = 0 ; j < niveles_max; j++)
				{
					if(j == nivel_actual)
					{
						niveles[j].SetActive(true);
						fichas[j].SetActive(true);
					}
					else 
					{
						niveles[j].SetActive(false);
						fichas[j].SetActive(false);
					}
				}
			}

		}
	}


	///
	

	public void Comenzar ()
	{
		MENU.SetActive(false);
		GAME.SetActive(true);

	}
	public void final()
	{
		Application.LoadLevel(1);
	}
}
