using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class DialogueTextBox : UIComposite
    {

        public System.Drawing.RectangleF mouseBox;
        public TextArea label;


        public LiveEntity lent;
        public Vector2 textOffset;

        public Dialogue currentDialogue;

        public DialogueTextBox() 
        {
            type = UICompositeType.DIALOGUE_TEXT_BOX;

             
            Vector2 boxSize = new Vector2(Globals.camera.viewport.Width*0.75f, 160);
            float YBottomMargin = 32;
            Vector2 boxPos = new Vector2((Globals.camera.viewport.Width - boxSize.X)/2, Globals.camera.viewport.Height - boxSize.Y - YBottomMargin);
            this.position = boxPos;
            Frame frame = new Frame(boxPos, boxSize);
            children.Add(frame);


            Vector2 mouseboxPos = new Vector2(position.X - Globals.camera.viewport.Width / 2, position.Y - Globals.camera.viewport.Height / 2);
            mouseBox = new System.Drawing.RectangleF(mouseboxPos.X, mouseboxPos.Y, boxSize.X, boxSize.Y);


            if (Globals.player.interractedEntity != null)
            {
                if(Globals.player.interractedEntity is LiveEntity lent)
                {
                    this.lent = lent;
                    string hint = lent.name;


                    Vector2 npcCIHoffset = new Vector2(12, 12);
                    CharacterIconHolder npcCIH = new CharacterIconHolder(lent, position + npcCIHoffset, Vector2.One, null, hint, 0);
                    children.Add(npcCIH);

                    textOffset = position + npcCIHoffset;
                    textOffset.X += npcCIH.charSprite.size.X;

                    currentDialogue = Globals.dialogueData.GetDialogue(lent.name, lent.currentDialogueId);
                    RefreshTextArea();
                }
            }


           
        }
        public override void Update()
        {
            if (mouseBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
            {
                if (Globals.inputManager.IsMouseButtonClick(InputManager.MouseButton.Left))
                {
                    var nextDialogue = Globals.dialogueData.GetDialogue(lent.name, lent.currentDialogueId+1);
                    if (nextDialogue != null)
                    {
                        currentDialogue = nextDialogue;
                        lent.currentDialogueId = nextDialogue.Id;
                        RefreshTextArea();
                    }
                    else
                    {
                        lent.EndDialouge();
                    }
                }


            }
            base.Update();
        }



        public void RefreshTextArea()
        {
            children.Remove(label);
            label = new TextArea(currentDialogue.Text, textOffset, 0, Color.White, null, (int)mouseBox.Width, (int)mouseBox.Height);
            children.Add(label);
        }
    }
}
