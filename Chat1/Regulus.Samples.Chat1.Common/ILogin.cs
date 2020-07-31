namespace Regulus.Samples.Chat1.Common
{
    public interface ILogin
    {
        Regulus.Remote.Value<bool> Login(string name);        
    }
    
}
