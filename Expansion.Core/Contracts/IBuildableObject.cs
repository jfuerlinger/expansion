namespace Expansion.Core.Contracts;

public interface IBuildableObject : ITimeBound, IIdentifyableObject
{
  public int TicksToBeBuilt { get; }
  public double BuildProgressPercentage { get; }
}