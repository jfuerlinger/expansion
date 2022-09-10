using Expansion.Core.Contracts;

namespace Expansion.Core.Infrastructure
{
  public abstract class BuildableObjectBase : IdentifyableObjectBase, IBuildableObject
  {
    private int _buildProgressTicks = 0;
    private bool _buildInProgress = true; 

    public abstract int TicksToBeBuilt { get; }

    public double BuildProgressPercentage => 100.0 / TicksToBeBuilt * _buildProgressTicks;

    public virtual void Tick()
    {
      if(_buildInProgress)
      {
        _buildProgressTicks++;

        if(_buildProgressTicks >= TicksToBeBuilt)
        {
          _buildInProgress = false;
        }
      }
    }
  }
}
