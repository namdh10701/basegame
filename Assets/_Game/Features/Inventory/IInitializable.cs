namespace _Game.Features.Inventory
{
    interface IInitializable
    {
        void Initialize();
        
        bool IsInitialized { get; }
    }
}