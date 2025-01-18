using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.World.Textures;

public class WorldMapTextures
{
    public TexturesTilemap TexturesTilemap { get; }
    public TexturesStreet TexturesStreet { get; }

    public WorldMapTextures(Game game)
    {
        this.TexturesTilemap = new TexturesTilemap(game);
        this.TexturesStreet = new TexturesStreet(game);
    }
}