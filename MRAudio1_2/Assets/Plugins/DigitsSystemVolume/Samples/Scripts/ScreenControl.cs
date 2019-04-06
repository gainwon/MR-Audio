using UnityEngine;

namespace DigitsSystemVolume.Samples
{
	public class ScreenControl: MonoBehaviour
	{
		[SerializeField]
		private GainwonVolumeController view;

		[SerializeField]
		private AudioSource audioPlayer;

		private void Start()
		{
            if (NativeSystemVolumeManager.IsCurrentPlatformSupported())
            {
                view.HideErrorLabel();

                AudioStreamType[] audioStreamTypes = NativeSystemVolumeManager.GetSupportedAudioStreamTypes();
                AudioOutputDevice[] audioOutputDevices = NativeSystemVolumeManager.GetAudioOutputDevices();
                view.InitializeUI(audioStreamTypes, audioOutputDevices);
            }
            else
            {
                view.ShowErrorLabel();

                Debug.LogWarningFormat("Native System Volume is not supported on this platform.");
            }
		} 
	}
}
