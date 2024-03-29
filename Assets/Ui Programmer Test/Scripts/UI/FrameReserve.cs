using TMPro;
using UnityEngine;

namespace UiProgrammerTest.Scripts.UI
{
    /// <summary>
    /// UI ������ � ������� ����, ���������� ���������� ���������� �������
    /// </summary>
    public class FrameReserve : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCount;
        [SerializeField] private GameModel.ConsumableTypes _consumableType;



        private void Awake()
        {
            UpdateReserveCount();

            GameModel.OperationComplete += UpdateReserveCount;
        }

        private void OnDestroy()
        {
            GameModel.OperationComplete -= UpdateReserveCount;
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
