using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class BattleBackground
    {
        private List<Sprite> layers;       // List of sprites for each layer
        private List<float> layerSpeeds;   // Speed multiplier for each layer
        private List<float> positionsX;    // X positions for each layer
        private Vector2 camerapos;         // Current camera position
        public int setID;                  // Identifier for the background set
        public float foreGroundWidth;

        public BattleBackground(int setID)
        {
            this.setID = setID;

            // Initialize layers and their speeds
            layers = new List<Sprite>();
            layerSpeeds = new List<float>();
            positionsX = new List<float>();

            // Load background, midground, and foreground sprites
            Sprite background = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.battle_backgrounds, 0, new Vector2(0, setID * 720), new Vector2(1280, 720));
            Sprite midbackground = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.battle_backgrounds, 2, new Vector2(0, setID * 720), new Vector2(1280 * 1.1f, 720));
            Sprite midground = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.battle_backgrounds, 1, new Vector2(0, setID * 720), new Vector2(1600, 720));
            Sprite foreground = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.battle_backgrounds, 3, new Vector2(0, setID * 720), new Vector2(1920, 720));

            foreGroundWidth = foreground.Width;

            layers.Add(background);
            layers.Add(midbackground);
            layers.Add(midground);
            layers.Add(foreground);

            // Set speeds for parallax scrolling effect (adjust these values as needed)
            layerSpeeds.Add(1.0f);  // background moves slower
            layerSpeeds.Add(0.8f);
            layerSpeeds.Add(0.6f);  // midground moves at medium speed
            layerSpeeds.Add(0.05f);  // foreground moves faster

            // Initialize positions based on initial camera position
            camerapos = new Vector2(Globals.camera.position.X, Globals.camera.position.Y / 2);

            for (int i = 0; i < layers.Count; i++)
            {
                positionsX.Add(camerapos.X * layerSpeeds[i]);
            }
        }

        public void Update()
        {
            // Update camera position
            camerapos.X = Globals.camera.position.X - Globals.camera.viewport.Width / 2;

            // Update positions of each layer based on camera movement and layer speed
            for (int i = 0; i < layers.Count; i++)
            {
                positionsX[i] = camerapos.X * layerSpeeds[i];
            }
        }

        public void Draw()
        {
            // Draw each layer
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Draw(new Vector2(positionsX[i], 0), Color.White, 0, Vector2.Zero, Vector2.One, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0); // Draw at Y position 0 (assuming vertically centered)
            }
        }
    }
}
