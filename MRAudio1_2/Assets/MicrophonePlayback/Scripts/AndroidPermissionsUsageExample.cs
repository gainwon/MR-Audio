using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AndroidPermissionsUsageExample : MonoBehaviour
{
    public AudioSource audioPlayer;

    private void Start()
    {
        OnGrantButtonPress();
    }

    public void MicInput()
    {
        audioPlayer.clip = Microphone.Start(null, true, 1, 22050);
        audioPlayer.loop = true;
        while (!(Microphone.GetPosition(null) > 1)) { }
        audioPlayer.Play();
    } 

    private const string RECORDAUDIO_PERMISSION = "android.permission.RECORD_AUDIO"; 
    public void OnGrantButtonPress()
    { 
        AndroidPermissionsManager.RequestPermission(new[] { RECORDAUDIO_PERMISSION }, new AndroidPermissionCallback(
             grantedPermission =>
             { 
             },
            deniedPermission =>
            { 
            },
            deniedPermissionAndDontAskAgain =>
            { 
            }));  
    } 

    private bool CheckPermissions()
    { 
        return AndroidPermissionsManager.IsPermissionGranted(RECORDAUDIO_PERMISSION);
    }
}
