using Expansion.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expansion.Core.Infrastructure
{
  public class Game
  {
    private static Game? _instance;

    private readonly List<BuildOrder> _buildOrders = new List<BuildOrder>();
    private readonly List<IGameObject> _gameObjects = new List<IGameObject>();

    private readonly List<BuildOrder> _buildOrdersToBeRemovedFromQueue = new List<BuildOrder>();

    public static Game Instance => _instance ??= new Game();

    private Game()
    {
      GameTimer.Instance.TickOccured += TickOccured;
    }

    private void TickOccured(TimerEventArgs e)
    {
      Debug.WriteLine("---- Tick occured ----");

      foreach (ITimeBound entry in _gameObjects.OfType<ITimeBound>())
      {
        entry.Tick();
      }

      foreach (BuildOrder buildOrder in _buildOrders)
      {
        buildOrder.Tick();
      }

      Debug.WriteLine("  Tick propageted");

      if (_buildOrdersToBeRemovedFromQueue.Any())
      {
        foreach (BuildOrder buildOrderToBeRemoved in _buildOrdersToBeRemovedFromQueue)
        {
          _buildOrders.Remove(buildOrderToBeRemoved);
        }

        _buildOrdersToBeRemovedFromQueue.Clear();

        Debug.WriteLine("  BuildOrder cleanup performed");
      }

    }

    public void BuildObject(IBuildableObject buildableObject)
    {
      _gameObjects.Add(buildableObject);

      var buildOrder = new BuildOrder(buildableObject);
      buildOrder.BuildOrderDone += BuildOrder_BuildOrderDone;
      _buildOrders.Add(buildOrder);
    }

    private void BuildOrder_BuildOrderDone(object? sender, BuildOrderDoneEventArgs e)
    {
      BuildOrder buildOrder = (sender as BuildOrder)!;

      Logger.Instance.LogInfo($"Build Order done: {sender}");

      _buildOrdersToBeRemovedFromQueue.Add(buildOrder);
    }
  }
}
