using System.Text;

namespace My_BoomSosed_NET
{
    class Config
    {
        string file = "config.cfg";
        Logger logger;
        Dictionary<string, string> config;
        public Config(Logger logger )
        {
            this.logger = logger;
            config = new Dictionary<string, string>();
            Load();
        }
        public void Load()
        {
            if (File.Exists(file))
            {
                string[] lines;
                lines = File.ReadAllLines(file);
                config.Clear();
                foreach (var line in lines)
                {
                    string[] _ = line.Trim().Split("=");
                    config.Add( _[0].Trim(), _[1].Trim().Replace("***-***","") );
                }
            }
            else 
            {
                logger.Add($"Config {file} not found.");
            }
        }
        public void Add(string key, string val)
        {
            if (config.ContainsKey(key))
            {
                config[key] = val;
            }
            else
            {
                config.Add(key, val);
            }
        }
        public string Get(string key)
        {
            string retval = "";
            if (config.ContainsKey(key))
            {
                retval = config[key];
            }
            return retval;
        }
        public void Save()
        {
            string[] lines = new string[config.Count];
            int i = 0;
            foreach (var conf in config)
            {
                logger.Add($"{conf.Key} = ***-***{conf.Value}***-***");
                lines[i] = $"{conf.Key} = ***-***{conf.Value}***-***";
                i++;
            }           
            
            File.WriteAllLines(file, lines, Encoding.UTF8);
        }
    }
}
