using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamJRPG
{
    public class QuestBookInGameMenu : UIComposite
    {

        public string[] pageNames = new string[] { "Primary Quests", "Secondary Quests", "Additional Quests", "Completed Quests" };


        public int pageID = 0;

        public ButtonChoicePanel questBCP;

        public List<UIComposite> topBar;
        public List<UIComposite> questList;
        public List<UIComposite> questDesc;

        public Vector2 questListPos;
        public Vector2 questListSize;



        public int currentQuestId = 0;

        public QuestBookInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_QUESTBOOK;


            //frame
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f, 10);


            Frame frame = new Frame(framePos, frameSize);

            children.Add(frame);


            //top label
            Vector2 textSize = Globals.assetSetter.fonts[2].MeasureString(this.pageNames[0]);

            topBar = new List<UIComposite>();
            RefreshPageName();
            

            
            //quest list frame
            framePos = new Vector2(framePos.X, framePos.Y + textSize.Y + 16);
            Frame questFrame = new Frame(framePos, new Vector2(frameSize.X/3, frameSize.Y - textSize.Y - 16));
            children.Add(questFrame);

            questListPos = framePos;
            questListSize = frameSize;



            questList = new List<UIComposite>();


            //quest descript frame
            framePos = new Vector2(framePos.X + frameSize.X / 3 + 36, framePos.Y);
            questFrame = new Frame(framePos, new Vector2(frameSize.X / 3 * 2 - 36, frameSize.Y - textSize.Y - 16));
            children.Add(questFrame);


            questDesc = new List<UIComposite>();

            RefreshQuestList();
            RefreshQuestContent();

        }




        public override void Update()
        {

            if (Globals.group.actualQuests.Count > 0)
            {


                if (questBCP.Changed)
                {
                    RefreshPageName();
                    RefreshQuestList();
                    RefreshQuestContent();
                }
            }
            


            base.Update();
        }


        public void RefreshPageName()
        {
            for (int i = 0; i < topBar.Count; i++)
            {
                children.Remove(topBar[i]);
            }

            topBar.Clear();

            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f, 10);
            Vector2 textSize = Globals.assetSetter.fonts[2].MeasureString(this.pageNames[0]);

            Label pageName = new Label(this.pageNames[0], new Vector2(framePos.X + frameSize.X / 2 - textSize.X / 2, framePos.Y), 2, Color.White, null);

            topBar.Add(pageName);


            // Top arrows
            Sprite buttonTexture = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(0, 32 * 3), new Vector2(32, 32));
            Vector2 buttonOffset = new Vector2(20, 0);

            Button leftArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width / 2 + pageName.components[0].position.X - buttonTexture.srcRect.Width - buttonOffset.X, framePos.Y + textSize.Y / 2 - buttonTexture.srcRect.Height / 3), 1, 15, "Previous");
            for (int i = 0; i < leftArrow.children[0].components.Count; i++)
            {
                leftArrow.children[0].components[i].spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Button rightArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width / 2 + pageName.components[0].position.X + textSize.X + buttonOffset.X, framePos.Y + textSize.Y / 2 - buttonTexture.srcRect.Height / 3), 1, 16, "Next");

            topBar.Add(leftArrow);
            topBar.Add(rightArrow);

            children.AddRange(topBar);
        }


        public void RefreshQuestContent()
        {
            for (int i = 0; i < questDesc.Count; i++)
            {
                children.Remove(questDesc[i]);
            }

            questDesc.Clear();

            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f + frameSize.X / 3 + 36, 10 + 20 + 30);
            Vector2 questFrameSize = new Vector2(frameSize.X / 3 * 2 - 36, frameSize.Y - 20 - 16);

            TextArea questTitle = new TextArea(Globals.group.actualQuests[currentQuestId].name, new Vector2(framePos.X, framePos.Y), 1, Color.White, null, (int)(frameSize.X / 3 * 2 - 36), (int)(frameSize.Y - 20 - 16));
            questDesc.Add(questTitle);

            TextArea questDescription = new TextArea(Globals.group.actualQuests[currentQuestId].description, new Vector2(framePos.X, framePos.Y + questTitle.textSize.Y + 40), 0, Color.White, null, (int)(frameSize.X / 3 * 2 - 36), (int)(frameSize.Y - 20 - 16));
            questDesc.Add(questDescription);

            children.AddRange(questDesc);
        }



        public void RefreshQuestList()
        {
            for (int i = 0; i < questList.Count; i++)
            {
                children.Remove(questList[i]);
            }

            questList.Clear();


            //quest list
            Button[] buttonArray = new Button[Globals.group.actualQuests.Count];
            Vector2 Offset = new Vector2(10, 64);

            for (int i = 0; i < Globals.group.actualQuests.Count; i++)
            {


                Color selectedColor = Color.White;

                if (questBCP != null)
                {

                    if (questBCP.currentChoice == i + 30)
                    {
                        selectedColor = Color.Yellow;
                        currentQuestId = i;
                    }
                    else
                    {
                        selectedColor = Color.White;
                    }
                }


                string truncatedName = TruncateQuestName(Globals.group.actualQuests[i].name, Globals.assetSetter.fonts[1], questListSize.X / 3 - 20);
                TextButton txtBtn = new TextButton(truncatedName, new Vector2(questListPos.X + Offset.X, questListPos.Y + i * Offset.Y), 1, 30 + i, selectedColor);
                

                buttonArray[i] = txtBtn;

            }



                ScrollableFrame questScroll = new ScrollableFrame(new Vector2(questListPos.X + Offset.X, questListPos.Y + 20), new Vector2(questListSize.X / 3, questListSize.Y - 20 * 3), 1, buttonArray[0].frameSize);
                questScroll.children.AddRange(buttonArray);
                questList.Add(questScroll);

                questBCP = new ButtonChoicePanel(buttonArray);
                questList.Add(questBCP);
            
            



            


            children.AddRange(questList);
        }







        private string TruncateQuestName(string questName, SpriteFont font, float maxWidth)
        {
            string ellipsis = "...";
            float ellipsisWidth = font.MeasureString(ellipsis).X;

            if (font.MeasureString(questName).X <= maxWidth)
            {
                return questName;
            }

            int length = questName.Length;
            while (length > 0 && font.MeasureString(questName.Substring(0, length) + ellipsis).X > maxWidth)
            {
                length--;
            }

            return questName.Substring(0, length) + ellipsis;
        }











    }
}
