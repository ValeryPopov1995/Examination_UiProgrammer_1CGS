namespace UiProgrammerTest.Scripts.UI.Counters
{
    public class CreditUiCounter : UiCounter
    {
        protected override int Value => GameModel.CreditCount;

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