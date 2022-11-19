namespace PrincessAndContenders;

[Serializable]
public class UnknownContenderException : Exception
{
    public UnknownContenderException() { }
    public UnknownContenderException(string message) : base(message) { }
    public UnknownContenderException(string message, Exception inner) : base(message, inner) { }
}