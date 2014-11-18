using UnityEngine;
using System.Collections;

public class DreamCatcherHandler : MonoBehaviour
{
    double timer;

    // Use this for initialization
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            this.gameObject.SetActive(false);
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        timer = 5;
    }

    void OnTriggerEnter2D (Collider2D Other) {
        HiddenEntity hiddenEntity = Other.gameObject.GetComponent<HiddenEntity>();

        if (hiddenEntity != null) {
            hiddenEntity.Reveal();
        }
    }
}
