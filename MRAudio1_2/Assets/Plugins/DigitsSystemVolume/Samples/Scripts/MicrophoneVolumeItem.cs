using UnityEngine;

namespace DigitsSystemVolume.Samples
{
    public class MicrophoneVolumeItem : VolumeItem
    {
        [SerializeField]
        private AudioSource audioPlayer;

        public void OnSliderValueChanged(float value)
        {
            if (percentageLabel != null)
            {
                percentageLabel.text = Mathf.RoundToInt(value * 100) + "%";
                audioPlayer.volume = value;
            }
        }

        public void OnMuteClick()
        {
            muted = !muted;

            audioPlayer.mute = muted;

            UpdateMuteSprite();
        } 

       
    }
}
