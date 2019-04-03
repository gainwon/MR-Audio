// Help needed! How to reduce latency? Thanks Thanks Thanks :) 
     
// This is the main function. When the user is ready to record MicInput() will be called.
// Summary: start recording audio, and play it back using an AudioSource component.
// Current Issue: Small latency between capturing the audio and playing it back.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AndroidPermissionsUsageExample : MonoBehaviour
{

    public Text text;
     
    // *** VVV *** //
    public void MicInput()
    {
        text.text = "Start Rec..";
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, 22050);
        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        text.text = "start playing... position is " + Microphone.GetPosition(null);
        audio.Play();
    }

    public void BeginListener(int index)
    {
        int min = 0;
        int max = 0;

        AudioSource _AudioSource = GetComponent<AudioSource>();

        Microphone.GetDeviceCaps(Microphone.devices[index], out min, out max);


        _AudioSource.clip = Microphone.Start(Microphone.devices[index], true, 2, max);

        while (!(Microphone.GetPosition(Microphone.devices[index]) > 1))
        {
            // Wait until the recording has started
        }

        _AudioSource.loop = true;
        _AudioSource.Play();
    }

    private const string RECORDAUDIO_PERMISSION = "android.permission.RECORD_AUDIO";



    public void OnGrantButtonPress()
    {
        text.text = "On Grant..";
        AndroidPermissionsManager.RequestPermission(new[] { RECORDAUDIO_PERMISSION }, new AndroidPermissionCallback(
             grantedPermission =>
             {
                 text.text = "Accepted"; 
             },
            deniedPermission =>
            {
                // The permission was denied
            },
            deniedPermissionAndDontAskAgain =>
            {
                // The permission was denied, and the user has selected "Don't ask again"
                // Show in-game pop-up message stating that the user can change permissions in Android Application Settings
                // if he changes his mind (also required by Google Featuring program)
            }));  
    }


    private bool CheckPermissions()
    { 
        return AndroidPermissionsManager.IsPermissionGranted(RECORDAUDIO_PERMISSION);
    }
}
