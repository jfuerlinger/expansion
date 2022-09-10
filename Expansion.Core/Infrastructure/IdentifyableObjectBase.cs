using Expansion.Core.Contracts;

namespace Expansion.Core.Infrastructure
{
  public class IdentifyableObjectBase : IIdentifyableObject
  {
    private Guid _id;

    public Guid Id => _id;

    public IdentifyableObjectBase()
    {
      _id = Guid.NewGuid();
    }

    public override string ToString() => $"Id: {_id}";
  }
}
