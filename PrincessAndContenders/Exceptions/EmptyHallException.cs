namespace PrincessAndContenders.Exceptions;

[Serializable]
public class EmptyHallException : Exception
{
    public EmptyHallException() { }
    public EmptyHallException(string message) : base(message) { }
    public EmptyHallException(string message, Exception inner) : base(message, inner) { }
}