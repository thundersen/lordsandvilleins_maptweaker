using BepInEx.Configuration;

namespace WorldTweaker
{
    class WorldTweakerConfig
    {
        internal int WorldSize { get; private set; }

        internal WorldTweakerConfig(ConfigFile config)
        {
            WorldSize = config.Bind("world", "size", 128, "World size in tiles").Value;
        }
    }
}