using PrimeTween;
using TMPro;
using UnityEngine;

namespace Code
{
    public class MoneyUI : MonoBehaviour
    {
        [Header("Main UI")] 
        [SerializeField] 
        private TextMeshProUGUI moneyText;

        [Header("Feedback")] 
        [SerializeField] 
        private RectTransform feedbackRoot;
        [SerializeField] 
        private TextMeshProUGUI feedbackTextPrefab;

        [Header("Animation")]
        [SerializeField] 
        private float flyDistance = 80f;
        [SerializeField] 
        private float duration = 0.8f;
        
        [SerializeField] 
        private Ease moveEase = Ease.OutCubic;

        [Header("Colors")]
        [SerializeField]
        private Color positiveColor = new(0.25f, 0.9f, 0.35f, 1f);
        [SerializeField]
        private Color negativeColor = new(1f, 0.3f, 0.3f, 1f);

        private void Awake()
        {
            RefreshMoneyText();
        }

        private void OnEnable()
        {
            Game.Instance.MoneyStorage.MoneyAdded += OnMoneyChanged;
        }

        private void OnDisabled()
        {
            Game.Instance.MoneyStorage.MoneyAdded -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int value)
        {
            RefreshMoneyText();
            ShowFeedback(value);
        }

        private void RefreshMoneyText()
        {
            moneyText.text = $"Money: {Game.Instance.MoneyStorage.CurrentMoney.ToString()}$";
        }

        private void ShowFeedback(int value)
        {
            var instance = Instantiate(feedbackTextPrefab, feedbackRoot);
            var rect = instance.rectTransform;

            bool isPositive = value >= 0;
            string prefix = isPositive ? "+" : string.Empty;

            instance.text = $"{prefix}{value}$";
            instance.color = isPositive ? positiveColor : negativeColor;

            Vector2 startPos = rect.anchoredPosition;
            Vector2 endPos = startPos + Vector2.down * flyDistance;

            Color startColor = instance.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

            Sequence.Create()
                .Group(Tween.UIAnchoredPosition(rect, endValue: endPos, duration: duration, ease: moveEase))
                .Group(Tween.Custom(instance, startColor, endColor, duration: duration,
                    onValueChange: static (target, color) => target.color = color))
                .OnComplete(target: instance.gameObject, static go => Destroy(go));
        }
    }
}