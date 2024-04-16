namespace _Base.Scripts.UI.Viewx
{
    public enum ViewState
    {
        Showing, Show, Hiding, Hide
    }
    public interface ICommand
    {
        public void Execute();
        public void Interrupt();
        public void OnCompleted();

        public void Initialize();
    }
}