using TMPro;
using UnityEngine;

namespace UiProgrammerTest.Scripts.UI.Buttons
{
    internal class BuyUiButtonForCredit : UiButton
    {
        [SerializeField] private GameModel.ConsumableTypes _type;
        [SerializeField] private TextMeshProUGUI _textCreditPrice;

        protected override void Awake()
        {
            base.Awake();
            var config = GameModel.ConsumablesPrice[_type];


            _textCreditPrice.text = config.CreditPrice.ToString();

            _button.onClick.AddListener(Buy);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Buy);
        }

        private void Buy()
        {
            GameModel.BuyConsumableForCredit(_type);
        }
    }
}
