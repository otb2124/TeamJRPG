

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TeamJRPG
{
    public class UIManager
    {

        public List<UIComposite> composites;


        public UIManager() 
        {
            composites = new List<UIComposite>();
        }


        public void Init()
        {
            
            AddTextFrame();
            AddCursor();

        }


        public void AddCursor()
        {
            composites.Add(new MouseCursor());
        }

        public void AddTextFrame()
        {
            composites.Add(new TextFrame("text", new Vector2(0, 0)));
        }



        public void RemoveAllCompositesOfTypes(params UIComposite.UICompositeType[] types)
        {
            foreach (var type in types)
            {
                foreach (var composite in composites.Where(c => c.type == type).ToList())
                {
                    composites.Remove(composite);
                }
            }
        }

        public void RemoveCompositeWithType(UIComposite.UICompositeType type)
        {

            for (int i = 0; i < composites.Count; i++)
            {
                if (composites[i].type == type)
                {
                    composites.Remove(composites[i]);
                    break;
                }
            }
        }





        public void Update()
        {
            for (int i = 0; i < composites.Count; i++)
            {
                composites[i].Update();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < composites.Count; i++)
            {
                composites[i].Draw();
            }
        }
    }
}
