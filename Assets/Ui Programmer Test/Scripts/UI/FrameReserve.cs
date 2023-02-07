using TMPro;
using UiProgrammerTest.Scripts.UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UiProgrammerTest.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class FrameReserve : MonoBehaviour
    {
        [SerializeField] private View _buyReserveView;
        [SerializeField] private TextMeshProUGUI _textCount;
        [SerializeField] private GameModel.ConsumableTypes _consumableType;
        private Button _button;



        private void Start()
        {
            _button = GetComponent<Button>();
            UpdateReserveCount();

            GameModel.OperationComplete += UpdateReserveCount;
            _button.onClick.AddListener(ShowBuyReservesView);
        }

        private void OnDestroy()
        {
            GameModel.OperationComplete -= UpdateReserveCount;
            _button.onClick.RemoveListener(ShowBuyReservesView);
        }



        private void ShowBuyReservesView()
        {
            _buyReserveView.Show();
        }

        private void UpdateReserveCount()
        {
            _textCount.text = GameModel.GetConsumableCount(_consumableType).ToString();
        }

        private void UpdateReserveCount(GameModel.OperationResult operationResult)
        {
            if (!operationResult.IsSuccess) return;

            _textCount.text = GameModel.GetConsumableCount(_consumableType).ToString();
        }
    }
}
