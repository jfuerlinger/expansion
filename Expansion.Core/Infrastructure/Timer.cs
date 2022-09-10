namespace Expansion.Core.Infrastructure;

public class GameTimer
{
  private const double TimerSpeedFactorMin = 0.1;
  private const double TimerSpeedFactorMax = 10.0;

  private static GameTimer? _instance;
  private readonly System.Timers.Timer _internalTimer = new System.Timers.Timer();
  private double _timerSpeedFactor = 1.0;
  private int _cntOfTicksOccured;

  public static GameTimer Instance => _instance ??= new GameTimer();

  public double TimerSpeedFactor
  {
    get => _timerSpeedFactor;
    set
    {
      if (_timerSpeedFactor <= TimerSpeedFactorMin || _timerSpeedFactor > TimerSpeedFactorMax)
      {
        throw new InvalidOperationException($"The TimerSpeedFactor has to be a value between {TimerSpeedFactorMin:0.##} and {TimerSpeedFactorMax:0.##}");
      }

      _timerSpeedFactor = value;
      _internalTimer.Interval = 1000 / value;
    }
  }

  public bool IsRunning
  {
    get => _internalTimer.Enabled;
    private set
    {
      _internalTimer.Enabled = value;
    }
  }

  public event TickOccuredDelegate? TickOccured;
  public delegate void TickOccuredDelegate(TimerEventArgs args);

  private GameTimer()
  {
    InitTimer();
  }

  public void Start()
  {
    IsRunning = true;
  }

  public void Stop()
  {
    IsRunning = false;
  }

  private void InitTimer()
  {
    _internalTimer.Elapsed += (sender, args) =>
    {
      _cntOfTicksOccured++;
      OnTickOccured();
    };


    TimerSpeedFactor = 1.0;
    _internalTimer.Enabled = false;
  }

  protected virtual void OnTickOccured()
  {
    TickOccured?.Invoke(new TimerEventArgs(_cntOfTicksOccured));
  }
}