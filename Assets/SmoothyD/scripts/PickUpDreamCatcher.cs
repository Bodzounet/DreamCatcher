using UnityEngine;
using System.Collections;

public class PickUpDreamCatcher : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CharacterInventory>() != null)
        {
            other.gameObject.GetComponent<CharacterInventory>().dreamCatcher = true;
            Destroy(this.gameObject);
        }
    }
}
