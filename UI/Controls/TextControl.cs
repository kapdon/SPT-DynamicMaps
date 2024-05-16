using InGameMap.Utils;
using TMPro;
using UnityEngine;

namespace InGameMap.UI.Controls
{
    public abstract class TextControl : MonoBehaviour
    {
        public TextMeshProUGUI Text { get; protected set; }
        public RectTransform RectTransform => gameObject.transform as RectTransform;

        public static T Create<T>(GameObject parent, string name, float fontSize) where T : TextControl
        {
            var go = UIUtils.CreateUIGameObject(parent, name);

            var textControl = go.AddComponent<T>();
            textControl.Text = go.AddComponent<TextMeshProUGUI>();
            textControl.Text.autoSizeTextContainer = true;
            textControl.Text.fontSize = fontSize;
            textControl.Text.alignment = TextAlignmentOptions.Left;
            textControl.Text.outlineColor = new Color32(0, 0, 0, 255);
            textControl.Text.outlineWidth = 0.15f;
            textControl.Text.fontStyle = FontStyles.Bold; // for some reason this refreshes the outline and applies the outline

            return textControl;
        }
    }
}