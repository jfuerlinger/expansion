namespace Expansion.Core
{
  public class TimerEventArgs : EventArgs
  {
    public int TicksOccured { get; init; }

    public TimerEventArgs(int ticksOccured)
    {
      TicksOccured = ticksOccured;  
    }
  }
}
