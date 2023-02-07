using TMPro;
using UnityEngine;

namespace UiProgrammerTest.Scripts.UI.Buttons
{

    internal class BuyUiButtonForCoin : UiButton
    {
        [SerializeField] private GameModel.ConsumableTypes _type;
        [SerializeField] private TextMeshProUGUI _textCoinPrice;

        protected override void Awake()
        {
            base.Awake();
            var config = GameModel.ConsumablesPrice[_type];
            
            
            _textCoinPrice.text = config.CoinPrice.ToString();

            _button.onClick.AddListener(Buy);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Buy);
        }

        private void Buy()
        {
            GameModel.BuyConsumableForCoin(_type);
        }
    }
}
