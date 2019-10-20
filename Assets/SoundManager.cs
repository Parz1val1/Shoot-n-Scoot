using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static AudioSource music;
    public static AudioClip coin, hurt, shoot, hit, win, lose, gameMusic;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        music = this.GetComponent<AudioSource>();
        coin = (AudioClip)Resources.Load("Universal Sound FX/8BIT/Coin_Collect/8BIT_RETRO_Coin_Collect_Two_Note_Twinkle_Fast_mono");
        hurt = (AudioClip)Resources.Load("Universal Sound FX/8BIT/Hits_Bumps/8BIT_RETRO_Hit_Bump_Distorted_Fade_mono");
        shoot = (AudioClip)Resources.Load("Universal Sound FX/WEAPONS/Bow_Arrow/BOW_Release_Arrow_mono");
        hit = (AudioClip)Resources.Load("Universal Sound FX/WEAPONS/Bow_Arrow/ARROW_Hit_Metal_Armor_stereo");
        win = (AudioClip)Resources.Load("Universal Sound FX/PUZZLES/PUZZLE_Success_Brass_Fanfare_Bright_Wet_stereo");
        lose = (AudioClip)Resources.Load("Universal Sound FX/PUZZLES/PUZZLE_Success_Brass_Stab_Decline_Wet_stereo");
        gameMusic = (AudioClip)Resources.Load("Ultimate Game Music Collection/Puzzles/Lighthearted LOOP LONG");
        Debug.Log(gameMusic.name);
        music.clip = gameMusic;
        music.loop = true;
        music.volume = .5f;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
