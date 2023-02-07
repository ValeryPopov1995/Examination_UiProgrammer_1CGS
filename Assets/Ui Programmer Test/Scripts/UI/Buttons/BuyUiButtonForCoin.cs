using TMPro;
using UnityEngine;

namespace UiProgrammerTest.Scripts.UI.Buttons
{

    internal class BuyUiButtonForCoin : UiButton
    {
        [SerializeField] private GameModel.ConsumableTypes _type;
        [SerializeField] private TextMeshProUGUI _textCoinPrice;
        private GameModel.ConsumableConfig _config;

        protected override void Awake()
        {
            base.Awake();
            _config = GameModel.ConsumablesPrice[_type];
            
            _textCoinPrice.text = _config.CoinPrice.ToString();

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
            _button.interactable = _config.CoinPrice <= GameModel.CoinCount;
        }

        private void Buy()
        {
            GameModel.BuyConsumableForCoin(_type);
        }
    }
}
