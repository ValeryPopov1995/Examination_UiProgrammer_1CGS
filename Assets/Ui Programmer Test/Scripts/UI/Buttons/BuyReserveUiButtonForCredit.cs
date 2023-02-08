using TMPro;
using UnityEngine;

namespace UiProgrammerTest.Scripts.UI.Buttons
{
    /// <summary>
    /// Кнопка покупки Резервов за кредиты
    /// </summary>
    internal class BuyReserveUiButtonForCredit : UiButton
    {
        [SerializeField] private GameModel.ConsumableTypes _type;
        [SerializeField] private TextMeshProUGUI _textCreditPrice;
        private GameModel.ConsumableConfig _config;

        protected override void Awake()
        {
            base.Awake();
            _config = GameModel.ConsumablesPrice[_type];

            _textCreditPrice.text = _config.CreditPrice.ToString();

            GameModel.ModelChanged += SetInteractable;
            _button.onClick.AddListener(Buy);
            SetInteractable();
        }

        private void OnDestroy()
        {
            GameModel.ModelChanged -= SetInteractable;
            _button.onClick.RemoveListener(Buy);
        }

        private void SetInteractable()
        {
            _button.interactable = _config.CreditPrice <= GameModel.CreditCount;
        }

        private void Buy()
        {
            GameModel.BuyConsumableForCredit(_type);
        }
    }
}
