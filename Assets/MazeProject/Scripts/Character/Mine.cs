using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour 
{
	public AudioClip beep;
	public float startDisabledTime;
	public float countDownTime;
	public GameObject explosion;
	private bool enabled;
	private AudioSource audioSource;


	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
		StartCoroutine (StartDisabled (startDisabledTime));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (enabled)
		{
			if (collision.gameObject.tag.Equals ("Player")) 
			{
				StartCoroutine (CountDown (beep.length));
			}

		}
	}

	private IEnumerator StartDisabled(float time)
	{
		enabled = false;
		yield return new WaitForSeconds (time);
		enabled = true;
	}

	private IEnumerator CountDown(float time)
	{
		audioSource.PlayOneShot (beep);
		yield return new WaitForSeconds (time);
		Explode ();
	}

	private void Explode()
	{
		Instantiate (explosion);
		Destroy(this.gameObject);
	}
}
