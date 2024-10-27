using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TeamJRPG
{
    public class BattleStatus : UIComposite
    {
        public Sprite bgSprite;
        public Vector2 offset;
        public Vector2 reposition;
        public Vector2 scale;

        public ImageHolder[] images;
        public List<System.Drawing.RectangleF> rectangles;

        public FloatingInfoBox box;

        public bool boxOn;
        public int hadBox;

        public BattleStatus()
        {
            type = UICompositeType.BATTLE_STATUS;

            bgSprite = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 2, new Vector2(0, 64), new Vector2(32, 16));

            Sprite entitySprite = Globals.battleManager.all[0].sprites[0];
            scale = new Vector2(1.8f, 1.8f);
            float xOffset = (entitySprite.Width * Globals.gameScale - bgSprite.Width * scale.X) / 2;
            offset = new Vector2(xOffset, -12);

            images = new ImageHolder[3];
            rectangles = new List<System.Drawing.RectangleF>();
            for (int i = 0; i < Globals.battleManager.all.Count; i++)
            {
                rectangles.Add(new System.Drawing.RectangleF());
            }

            RefreshPointer();
        }

        public override void Update()
        {
                RefreshPointer();
                base.Update();
        }

        public void RefreshPointer()
        {
            children.Clear();

            for (int i = 0; i < Globals.battleManager.all.Count; i++)
            {

                LiveEntity entity = Globals.battleManager.all[i];

                if (entity.currentBattleStatus != LiveEntity.BattleStatus.dead)
                {
                    
                    position = entity.battleScreenPosition;

                    reposition = new Vector2(
                        position.X - Globals.camera.position.X + Globals.camera.viewport.Width / 2,
                        position.Y - Globals.camera.position.Y + Globals.camera.viewport.Height / 2
                    );

                    Vector2 boxPos = new Vector2(reposition.X - Globals.camera.viewport.Width / 2, reposition.Y - Globals.camera.viewport.Height / 2);

                    if(rectangles.Count > i)
                    {
                        rectangles[i] = new System.Drawing.RectangleF(boxPos.X, boxPos.Y, 32 * scale.X, 64 * scale.Y);

                        float hpWidth = 32f * (entity.currentHP / (float)entity.maxHP);
                        float manaWidth = 32f * (entity.currentMana / (float)entity.maxMana);

                        images[0] = new ImageHolder(bgSprite, reposition + offset, Color.White, scale, null);
                        images[1] = new ImageHolder(
                            Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 2, new Vector2(32, 64), new Vector2(MathHelper.Clamp(hpWidth, 0, 32), 16)),
                            reposition + offset,
                            Color.White,
                            scale,
                            null
                        );
                        images[2] = new ImageHolder(
                            Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 2, new Vector2(64, 64), new Vector2(MathHelper.Clamp(manaWidth, 0, 32), 16)),
                            reposition + offset,
                            Color.White,
                            scale,
                            null
                        );

                        for (int j = 0; j < images.Length; j++)
                        {
                            children.Add(images[j]);
                        }

                        System.Drawing.PointF cursorPos = new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y);

                        if (rectangles[i].Contains(cursorPos))
                        {
                            if (!boxOn)
                            {
                                box = new FloatingInfoBox(new List<string> { entity.name, entity.currentHP + "/" + entity.maxHP, entity.currentMana + "/" + entity.maxMana }, new List<Color> { Color.White, Color.Red, Color.Blue });
                                Globals.uiManager.AddElement(box);
                                hadBox = i;
                                boxOn = true;
                            }
                        }
                        else if (boxOn && hadBox == i)
                        {
                            Globals.uiManager.RemoveElement(box);
                            box = null;
                            boxOn = false;
                        }
                    }
                    

                    
                }
                
            }
        }
    }
}
