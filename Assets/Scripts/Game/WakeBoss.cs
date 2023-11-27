using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeBoss : MonoBehaviour
{
    public Valsorim boss;
    public Camera cam;
    public AudioClip boss_music;
    public GameObject boss_hp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player has triggered the boss battle
        if (other.gameObject.tag == "Player") {
            cam.GetComponent<CameraFollow>().target2 = boss.gameObject;
            boss.GetComponent<Animator>().SetTrigger("Phase1");
            AudioSource music = GameObject.Find("MusicBox").GetComponent<AudioSource>();
            music.clip = boss_music;
            music.Play();
            boss_hp.SetActive(true);
        }
    }
}
