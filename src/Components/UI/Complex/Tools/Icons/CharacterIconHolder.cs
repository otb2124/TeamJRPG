using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class CharacterIconHolder : UIComposite
    {
        public LiveEntity entity;

        public Vector2 adjustedPosition;
        public Vector2 scale;

        public Sprite charSprite;

        public List<UIComposite> bars;

        public CharacterIconHolder(LiveEntity entity, Vector2 startPosition, Vector2 scale, Stroke stroke, string hint, int frameType)
        {
            this.entity = entity;
            adjustedPosition = startPosition;
            charSprite = entity.characterIcon;
            this.scale = scale;

            ImageHolder backGround = new ImageHolder(entity.backGroundIcon, adjustedPosition, Color.White, scale * 2, frameType == 0 ? stroke : null);
            ImageHolder charac = new ImageHolder(charSprite, adjustedPosition, entity.skinColor, scale, null);
            charac.floatingText.Clear();
            if (hint != null)
            {
                charac.floatingText.Add(hint);
                charac.floatingTextColors.Clear();
                charac.floatingTextColors.Add(Color.White);
            }
            

            Sprite frameTexture = frameType switch
            {
                0 => Globals.textureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, Vector2.Zero, new Vector2(32, 32)),
                1 => Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(0, 32 * 5), new Vector2(64, 64)),
                2 => Globals.textureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, Vector2.Zero, new Vector2(32, 32)),
                _ => null
            };
            ImageHolder frame = new ImageHolder(frameTexture, adjustedPosition, Color.White, scale, stroke);

            children.Add(backGround);
            children.Add(charac);
            children.Add(frame);

            if (frameType == 1 || frameType == 2)
            {
                
                charac.floatingText.Add(entity.currentHP + "/" + entity.maxHP);
                charac.floatingTextColors.Add(Color.Red);
                charac.floatingText.Add(entity.currentMana + "/" + entity.maxMana);
                charac.floatingTextColors.Add(Color.Blue);

                if(frameType == 1)
                {
                    bars = new List<UIComposite>();
                    RefreshBars();
                }
                
            }

            foreach (var component in components)
            {
                component.IsStickToCamera = true;
                component.IsStickToZoom = true;
            }
        }


        public override void Update()
        {

            if (bars != null)
            {

                RefreshBars();
            }


            base.Update();
        }



        public void RefreshBars()
        {

            for (int i = 0; i < bars.Count; i++)
            {
                children.Remove(bars[i]);
            }

            bars.Clear();

            Sprite hp = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(32 * 2, 32 * 5), new Vector2(32, 64));
            float hpPercent = (float)entity.currentHP / entity.maxHP;
            int hpTexHeight = (int)(hpPercent * 64);
            hp.ResetSrcRect(hp.sheetPosition, new Vector2(hp.Width, hpTexHeight));
            ImageHolder hpHolder = new ImageHolder(hp, adjustedPosition, Color.White, scale, null);

            Sprite mana = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(32 * 3, 32 * 5), new Vector2(32, 64));
            float manaPercent = (float)entity.currentMana / entity.maxMana;
            int manaTexHeight = (int)(manaPercent * 64);
            mana.ResetSrcRect(mana.sheetPosition, new Vector2(mana.Width, manaTexHeight));
            ImageHolder manaHolder = new ImageHolder(mana, adjustedPosition + new Vector2(32 * scale.X, 0), Color.White, scale, null);

            bars.Add(hpHolder);
            bars.Add(manaHolder);

            children.AddRange(bars);
        }
    }
}
