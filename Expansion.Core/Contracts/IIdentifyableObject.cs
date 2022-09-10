namespace Expansion.Core.Contracts
{
  public interface IIdentifyableObject : IGameObject
  {
    public Guid Id { get; }
  }
}
