public class NullSeedException : Exception
{
    public NullSeedException() : base() {}
    public NullSeedException(string message) : base(message) {}
}