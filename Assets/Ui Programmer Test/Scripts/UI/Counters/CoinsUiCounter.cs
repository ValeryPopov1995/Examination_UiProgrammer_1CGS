namespace UiProgrammerTest.Scripts.UI.Counters
{
    /// <summary>
    /// UI счетчик монет
    /// </summary>
    public class CoinsUiCounter : UiCounter
    {
        protected override int Value => GameModel.CoinCount;

        protected override void Start()
        {
            base.Start();
            GameModel.OperationComplete += SetValue;
        }

        private void OnDestroy()
        {
            GameModel.OperationComplete -= SetValue;
        }
    }
}