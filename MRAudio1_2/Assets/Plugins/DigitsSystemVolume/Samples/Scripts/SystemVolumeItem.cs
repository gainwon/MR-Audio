using UnityEngine;

namespace DigitsSystemVolume.Samples
{
	public class SystemVolumeItem: VolumeItem
	{
		public AudioStreamType AudioStreamType { get; private set; }

		public void InitUI(AudioStreamType audioStreamType)
		{
			AudioStreamType = audioStreamType;
			slider.value = NativeSystemVolumeManager.GetSystemVolume(AudioStreamType);
			UpdateTitleLabel(audioStreamType.ToString());
			NativeSystemVolumeManager.AddSystemVolumeChangedListener(OnSystemVolumeChanged);
			NativeSystemVolumeManager.AddSystemVolumeMuteChangedListener(OnSystemVolumeMuteChanged);

			muted = NativeSystemVolumeManager.IsSystemVolumeMuted(audioStreamType);
			UpdateMuteSprite();
		}

		public void OnSliderValueChanged(float value)
		{
			if(percentageLabel != null)
			{
				percentageLabel.text = Mathf.RoundToInt(value * 100) + "%";
				NativeSystemVolumeManager.SetSystemVolume(value, AudioStreamType);
			}
		}

		public void OnMuteClick()
		{
			muted = !muted;

			if(muted)
			{
				NativeSystemVolumeManager.MuteSystemVolume(AudioStreamType);
			}
			else
			{
				NativeSystemVolumeManager.UnmuteSystemVolume(AudioStreamType);
			}

			UpdateMuteSprite();
		}

		public void OnSystemVolumeChanged(float volume, AudioStreamType audioStreamType)
		{
			if(slider != null && AudioStreamType == audioStreamType)
			{
				slider.value = volume;
			}
		}

		public void OnSystemVolumeMuteChanged(bool muted, AudioStreamType audioStreamType)
		{
			if(AudioStreamType == audioStreamType)
			{
				this.muted = muted;
				UpdateMuteSprite();
			}
		}
	}
}
