using Expansion.Core;
using Expansion.Core.Infrastructure;
using Expansion.Core.Model;

GameTimer gameTimer = GameTimer.Instance;
gameTimer.TickOccured += GameTimer_TickOccured;
gameTimer.TimerSpeedFactor = 5;

gameTimer.Start();
await Task.Delay(TimeSpan.FromSeconds(2));

Game.Instance.BuildObject(new FarmHouse());
Game.Instance.BuildObject(new FarmHouse());

await Task.Delay(TimeSpan.FromSeconds(1.5));

Game.Instance.BuildObject(new FarmHouse());

await Task.Delay(TimeSpan.FromSeconds(20));

Console.WriteLine("Press any key to exit!");
Console.ReadKey();


void GameTimer_TickOccured(TimerEventArgs e)
{
  Console.WriteLine($"Tick Occured - {e.TicksOccured}");
}