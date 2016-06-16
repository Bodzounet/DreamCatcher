using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {

    public Key.KeyType type;
    private GameObject j2;
    private GameObject j1;
    public AudioClip sound;
	// Use this for initialization
	void Start () 
    {
        j2 = GameObject.Find("CharacterRight");
        j1 = GameObject.Find("CharacterLeft");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.GetComponent<CharacterInventory>() != null && (type == Key.KeyType.SPECTRAL && j1.GetComponent<CharacterInventory>().key == type) || (type == Key.KeyType.NORMAL && j2.GetComponent<CharacterInventory>().key == type))
        {
            if (sound != null)
            {
                GameObject.Find("DreamEye").GetComponent<AudioSource>().clip = sound;
                GameObject.Find("DreamEye").GetComponent<AudioSource>().Play();
            }
            this.transform.parent.GetComponent<Collider2D>().enabled = false;
            this.GetComponentInParent<SpriteRenderer>().enabled = false;
            this.GetComponent<Collider2D>().enabled = false;
            c.GetComponent<CharacterInventory>().key = Key.KeyType.NO_KEY;
            if (this.transform.parent.parent != null)
                Destroy(this.transform.parent.parent.gameObject);
            else
                Destroy(this.transform.parent.gameObject);
        }
    }
}
