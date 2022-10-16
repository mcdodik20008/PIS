namespace PISWF.infrasrtucture.exeption;

public class StrangeError : Exception
{
    public string MethodName { get; }

    public string JsonObject { get; }

    public StrangeError(string message, string methodName, string jsonObject) : base(message)
    {
        MethodName = methodName;
        JsonObject = jsonObject;
    }
}