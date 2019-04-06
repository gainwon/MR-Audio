using UnityEngine;
using UnityEngine.UI;

namespace DigitsSystemVolume.Samples
{
    public class GainwonVolumeController : MonoBehaviour
    {
        private const float Y_SPACING = 64;

        [SerializeField]
        private SystemVolumeItem systemVolumeItemPrefab; 

        private Text errorLabel; 
        private ScrollRect scrollRect;
        private bool initialized;

        private void Awake()
        {
            if (!initialized) { Initialize(); }
        }

        private void Initialize()
        {
            errorLabel = transform.Find("ErrorLabel").GetComponent<Text>();
            playToggleLabel = transform.Find("PlayToggle/Label").GetComponent<Text>();
            scrollRect = GetComponentInChildren<ScrollRect>();
            initialized = true;
        }
        public Text text;
        public void InitializeUI(AudioStreamType[] audioStreamTypes, AudioOutputDevice[] audioOutputDevices)
        {
            if (!initialized) { Initialize(); }
            float y = -Y_SPACING;

            int length = audioStreamTypes.Length;
            for (int i = 0; i < length; i++)
            {
                AudioStreamType audioStreamType = audioStreamTypes[i];
                text.text += " - " + audioStreamType.ToString();
                if (audioStreamType.ToString() == "MUSIC" || audioStreamType.ToString() == "SYSTEM")
                {  
                    SystemVolumeItem volumeItem = CreateSystemVolumeItem();
                    volumeItem.InitUI(audioStreamType);
                    volumeItem.RectTransform.anchoredPosition = new Vector2(0, y);
                    y -= volumeItem.RectTransform.rect.height;
                    y -= Y_SPACING;
                } 
            } 

            scrollRect.content.sizeDelta = new Vector2(0, Mathf.Abs(y));
        }

        public SystemVolumeItem CreateSystemVolumeItem()
        {
            return (SystemVolumeItem)CreateVolumeItem(systemVolumeItemPrefab);
        } 

        public VolumeItem CreateVolumeItem(VolumeItem prefab)
        {
            VolumeItem volumeItem = GameObject.Instantiate(prefab);
            Vector2 sizeDelta = volumeItem.RectTransform.sizeDelta;
            volumeItem.RectTransform.SetParent(scrollRect.content);
            volumeItem.RectTransform.localScale = Vector3.one;
            volumeItem.RectTransform.localPosition = Vector3.zero;
            volumeItem.RectTransform.localRotation = Quaternion.identity;
            volumeItem.RectTransform.sizeDelta = sizeDelta;

            return volumeItem;
        } 

        public void ShowErrorLabel()
        {
            errorLabel.gameObject.SetActive(true);
        }

        public void HideErrorLabel()
        {
            errorLabel.gameObject.SetActive(false);
        }
    }
}