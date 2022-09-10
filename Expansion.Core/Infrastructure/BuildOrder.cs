using Expansion.Core.Contracts;

namespace Expansion.Core.Infrastructure
{
  internal class BuildOrder : ITimeBound
  {
    private readonly IBuildableObject _buildableObject;

    public BuildOrder(IBuildableObject buildableObject)
    {
      _buildableObject = buildableObject;
    }

    public event EventHandler<BuildOrderUpdateEventArgs>? BuildOrderUpdate;
    public event EventHandler<BuildOrderDoneEventArgs>? BuildOrderDone;

    protected virtual void OnBuildOrderUpdate(double percentage) => BuildOrderUpdate?.Invoke(this, new BuildOrderUpdateEventArgs(_buildableObject, percentage));
    protected virtual void OnBuildOrderDone() => BuildOrderDone?.Invoke(this, new BuildOrderDoneEventArgs(_buildableObject));

    public void Tick()
    {
      _buildableObject.Tick();
      OnBuildOrderUpdate(_buildableObject.BuildProgressPercentage);

      if (_buildableObject.BuildProgressPercentage == 100.0)
      {
        OnBuildOrderDone();
      }
    }

    public override string ToString()
    {
      return $"[BuildOrder for {_buildableObject}]";
    }
  }


  public record BuildOrderUpdateEventArgs(IBuildableObject BuildableObject, double Percentage);
  public record BuildOrderDoneEventArgs(IBuildableObject BuildableObject);
}
