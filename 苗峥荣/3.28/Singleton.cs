public class Singleton<T> where T : new()
{
    private static T _instance;

    public static T Instance {

        get
        {
            if (Equals(_instance, default(T)))
            {
                _instance = new T();
            }
            return _instance;
        }
    }
        
    public virtual void Init(){}
}