using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class DialogueResponses : UIComposite
    {


        public List<System.Drawing.RectangleF> rectList;
        public int Choice = -1;
        public List<TextArea> responses;
        public Dialogue currentDialogue;
        public Vector2 boxSize;

        public List<Color> colors;
        public Color hoverColor = Color.Orange;
        public Color defaultColor = Color.White;
        public bool IsRefreshed = false;

        public DialogueResponses(Dialogue currentDialogue) 
        {
            type = UICompositeType.DIALOGUE_TEXT_BOX;
            this.currentDialogue = currentDialogue;

            boxSize = new Vector2(Globals.camera.viewport.Width * 0.75f, 160);
            float YBottomMargin = 32;
            Vector2 boxPos = new Vector2((Globals.camera.viewport.Width - boxSize.X) / 2, Globals.camera.viewport.Height - boxSize.Y - YBottomMargin);
            Vector2 npcCIHoffset = new Vector2(12, boxSize.Y/3*2);

            this.position = boxPos + npcCIHoffset;

            CharacterIconHolder npcCIH = new CharacterIconHolder(Globals.player, position, Vector2.One, null, Globals.player.name, 0);
            children.Add(npcCIH);

            responses = new List<TextArea>();
            colors = new List<Color>();
            for (int i = 0; i < currentDialogue.Responses.Count; i++)
            {
                colors.Add(defaultColor);
            }

            RefreshResponses();
        }



        public override void Update()
        {

            for (int i = 0; i < rectList.Count; i++)
            {
                if (rectList[i].Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
                {
                    
                    if (!IsRefreshed)
                    {
                        colors[i] = hoverColor;
                        IsRefreshed = true;
                        RefreshResponses();
                    }

                    if (Globals.inputManager.IsMouseButtonClick(InputManager.MouseButton.Left))
                    {
                        Choice = i; break;
                        
                    }
                }
                else
                {
                    colors[i] = defaultColor;
                    if (IsRefreshed)
                    {
                        IsRefreshed = false;
                        RefreshResponses();
                    }
                }
            }

            base.Update();
        }



        public void RefreshResponses()
        {

                for (int i = 0; i < responses.Count; i++)
                {
                    children.Remove(responses[i]);
                }

                responses.Clear();

                rectList = new List<System.Drawing.RectangleF>();

                for (int i = 0; i < currentDialogue.Responses.Count; i++)
                {
                    Vector2 taPos = new Vector2(position.X + 64, position.Y + 32 * i);
                    TextArea ta = new TextArea(i + 1 + ") " + currentDialogue.Responses[i].Text, taPos, 0, colors[i], null, (int)boxSize.X, 16);
                    responses.Add(ta);


                    Vector2 recPos = new Vector2(taPos.X - Globals.camera.viewport.Width / 2, taPos.Y - Globals.camera.viewport.Height / 2);
                    rectList.Add(new System.Drawing.RectangleF(recPos.X, recPos.Y, boxSize.X, 32));
                }

                children.AddRange(responses);
            
            
        }
    }
}
