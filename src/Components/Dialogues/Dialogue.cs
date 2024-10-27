using System.Collections.Generic;
using System.Linq;

namespace TeamJRPG
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Response> Responses { get; set; }
        public int? NextDialogueId { get; set; } // This is used if there are no responses
    }

    public class Response
    {
        public string Text { get; set; }
        public int? NextDialogueId { get; set; }
    }

    public class NPCDialogues
    {
        public List<Dialogue> Dialogues { get; set; }
    }

    public class DialogueData
    {
        public Dictionary<string, NPCDialogues> NpcDialogues { get; set; }


        public Dialogue GetDialogue(string npcName, int dialogueId)
        {
            if (NpcDialogues.TryGetValue(npcName, out var npcDialogues))
            {
                return npcDialogues.Dialogues.FirstOrDefault(d => d.Id == dialogueId);
            }

            return null;
        }

        public Dialogue GetFirstDialogue(string npcName)
        {
            if (NpcDialogues.TryGetValue(npcName, out var npcDialogues))
            {
                return npcDialogues.Dialogues.FirstOrDefault();
            }

            return null;
        }

        public Response GetResponse(string npcName, int dialogueId, int responseIndex)
        {
            var dialogue = GetDialogue(npcName, dialogueId);
            if (dialogue != null && dialogue.Responses != null && responseIndex >= 0 && responseIndex < dialogue.Responses.Count)
            {
                return dialogue.Responses[responseIndex];
            }

            return null;
        }



        // DIALOGUES
        public void CloseDialogue()
        {
            Globals.currentGameState = Globals.GameState.playState;
            Globals.uiManager.currentMenuState = UIManager.MenuState.clean;
            Globals.uiManager.MenuStateNeedsChange = true;

            Globals.camera.focusEntity = Globals.player;
            Globals.camera.FocusOnEntity();
        }
    }
}
