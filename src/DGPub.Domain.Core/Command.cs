namespace DGPub.Domain.Core
{
    public abstract class Command
    {
        public abstract bool IsValid();
    }
}
