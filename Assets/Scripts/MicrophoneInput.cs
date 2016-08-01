using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]

public class MicrophoneInput : MonoBehaviour {
    //public float sensitivity = 100;
    //public float loudness = 0;

    //void Start() {
    //	GetComponent<AudioSource>().clip = Microphone.Start(null, true, 10, 44100);
    //	GetComponent<AudioSource>().loop = true; // Set the AudioClip to loop
    //	GetComponent<AudioSource>().mute = true; // Mute the sound, we don't want the player to hear it
    //	while (!(Microphone.GetPosition(null) > 0)){} // Wait until the recording has started
    //	GetComponent<AudioSource>().Play(); // Play the audio source!
    //}

    //void Update(){
    //	loudness = GetAveragedVolume() * sensitivity;

    //       Debug.Log(loudness);
    //}

    //float GetAveragedVolume()
    //{ 
    //	float[] data = new float[256];
    //	float a = 0;
    //	GetComponent<AudioSource>().GetOutputData(data,0);
    //	foreach(float s in data)
    //	{
    //		a += Mathf.Abs(s);
    //	}
    //	return a/256;
    //}

    public float MicLoudness;

    private string _device;

    //mic initialization
    void InitMic()
    {
        if (_device == null) _device = Microphone.devices[0];
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
    }

    void StopMicrophone()
    {
        Microphone.End(_device);
    }


    AudioClip _clipRecord = new AudioClip();
    int _sampleWindow = 128;

    //get data from microphone into audioclip
    float LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }



    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = LevelMax();
    }

    bool _isInitialized;
    // start mic when scene starts
    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
    }

    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }


    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");

            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
            _isInitialized = false;

        }
    }
}