using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UiProgrammerTest.Scripts.UI.Frames
{
    internal class CoinConverterFrame : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _convertCourseTextCredit;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TextMeshProUGUI _potentialCredits;
        [SerializeField] private Button _buyButton;
        private int _convrtCoins;

        private void Awake()
        {
            _convertCourseTextCredit.text = GameModel.CoinToCreditRate.ToString();
            _potentialCredits.text = "0";

            _inputField.onValueChanged.AddListener(ValidateInput);
            _buyButton.onClick.AddListener(Convert);
            GameModel.ModelChanged += ValidateInput;
            ValidateInput();
        }

        private void OnDestroy()
        {
            _inputField.onValueChanged.RemoveListener(ValidateInput);
            _buyButton.onClick.RemoveListener(Convert);
            GameModel.ModelChanged -= ValidateInput;
        }

        private void ValidateInput() => ValidateInput(_inputField.text);

        private void ValidateInput(string value)
        {
            if (value.Length > 0 && value[0] == '0')
                _inputField.text = value.Substring(1);
            else if (value.Length == 0)
            {
                _potentialCredits.text = "0";
                _buyButton.interactable = false;
            }

            if (value.Length > 0 && value[0] != '0')
                UpdateOffer();
        }

        private void UpdateOffer()
        {
            _convrtCoins = int.Parse(_inputField.text);

            if (_convrtCoins > GameModel.CoinCount)
            {
                _convrtCoins = GameModel.CoinCount;
                _inputField.text = _convrtCoins.ToString();
            }

            if (GameModel.CoinCount > 0 && _convrtCoins > 0)
            {
                _buyButton.interactable = true;
                _potentialCredits.text = (_convrtCoins * GameModel.CoinToCreditRate).ToString();
                return;
            }
        }

        private void Convert()
        {
            GameModel.ConvertCoinToCredit(_convrtCoins);
            _inputField.text = string.Empty;
        }
    }
}
