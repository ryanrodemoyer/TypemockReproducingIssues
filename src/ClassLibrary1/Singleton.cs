using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class CacheManager
    {
        private static volatile CacheManager _instance;

        private static readonly object _instanceLocker = new Object();

        private CacheManager()
        {
            Initialize();
        }

        private static bool _isInitialized = false;

        /// <summary>
        /// Initializes an instance of the CacheManager
        /// </summary>
        public static CacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            Console.WriteLine("INSTANCE CREATED!!!");

                            _instance = new CacheManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public static void Initialize()
        {
            _isInitialized = true;
        }

        private static Dictionary<string, string> _cache = new Dictionary<string, string>();

        public bool KeyExists(string key)
        {
            return _cache.ContainsKey(key);
        }

        public object GetData(string key)
        {
            return _cache[key];
        }

        public void ClearData()
        {
            _cache.Clear();
        }
    }

    public class ApiFacade
    {
        private CacheManager _cacheManager;

        private Api api = new Api();

        public ApiFacade()
        {
            _cacheManager = CacheManager.Instance;
        }

        public List<Team> GetTeams()
        {
            bool exists = _cacheManager.KeyExists("teams_data");
            if (exists)
            {
                var data = _cacheManager.GetData("teams_data");
                return (List<Team>)data;
            }

            return api.GetTeams();
        }
    }

    public class Api
    {
        public List<Team> GetTeams()
        {
            Thread.Sleep(750); // introduce fake delay

            return new List<Team>
            {
                new Team(1, "cougars"),
                new Team(2, "panthers"),
                new Team(3, "wild cats"),
            };
        }
    }

    public class Team
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Team(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
