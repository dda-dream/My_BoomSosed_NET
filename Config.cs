namespace My_BoomSosed_NET
{
    class Config
    {
        string file = "config.cfg";
        string[] lines;
        Logger logger;
        public Config(Logger logger )
        {
            lines = new string[] { };
            this.logger = logger;
            Load();
        }
        public void Load()
        {
            if (File.Exists(file))
            {
                lines = File.ReadAllLines(file);
            }
            else 
            {
                logger.Add($"Config {file} not found.");
            }
        }
        public void Save()
        {
            File.WriteAllLines(file, lines);
        }
    }
}
