using UnityEngine;



/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    
    private static object _lock = new object();
    
    
    public static T Instance
    {
        get
        {
//                      if (applicationIsQuitting) {
//                          //Debug.LogWarning("[Singleton] Instance '"+ typeof(T) +
////                                           "' already destroyed on application quit." +
////                                           " Won't create again - returning null.");
//                          return null;
//                      }
            
            lock(_lock)
            {
                if (_instance == null)
                {
                    // check if there is at least one instance in the scene 
                    _instance = (T) FindObjectOfType(typeof(T));
                    // if there is one, also check if there are multiple types which
                    // is not allowed
                    if ( _instance != null && FindObjectsOfType(typeof(T)).Length > 1 )
                    {
                        return null;
                    }
                    
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        singleton.hideFlags = HideFlags.HideInHierarchy;
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) "+ typeof(T).ToString();
                    } else {
                    }
                }
                
                return _instance;
            }
        }
    }
    
//    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
      public void OnDestroy () {
//          applicationIsQuitting = true;
      }
}




/*--------------- New singleton starts here ---------------*/

/*using UnityEngine;



/// <summary>
/// Be aware this will not prevent a non singleton constructor
/// such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
// public:
    public static T Instance {
        get {
            if (applicationIsQuitting) {
                return null;
            }
            
            lock (_lock) {
                if (_isInstantiated) {
                    return _instance;
                }

                System.Type type = typeof(T);
                T[] objects = FindObjectsOfType<T>();
                int count = objects.Length;

                if (count == 1) {
                    _instance = objects[0];
                    _isInstantiated = true;
                    return _instance;
                } else if (count > 1) {
                	return null;
                } else { // count == 0
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    _isInstantiated = true;
                    singleton.name = "(singleton) " + type;

                    DontDestroyOnLoad(singleton);
					//singleton.hideFlags = HideFlags.HideInHierarchy;
                    
                    return _instance;
                }
            }
        }

        private set {
            _instance = value;
            _isInstantiated = value != null;
        }
    }

// private:
    private static T _instance;
    private static bool _isInstantiated;
    private static object _lock = new object();
    private static bool applicationIsQuitting = false;
    private static bool isRestarting = false;

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    /// it will create a buggy ghost object that will stay on the Editor scene
    /// even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    private void OnDestroy() {
        applicationIsQuitting = true;
    }
}*/
