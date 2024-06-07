using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Project_HyperBoxer.Scripts.UI
{
    public class CombatControlView : WindowView
    {
        public PunchesPanel LeftPanel { get; }
        public PunchesPanel RightPanel { get; }
        public CombatControlView(UIDocument document) : base(document)
        {
            LeftPanel = new PunchesPanel(_rootDocument.rootVisualElement.Q<VisualElement>("LeftPanel"));
            RightPanel = new PunchesPanel(_rootDocument.rootVisualElement.Q<VisualElement>("RightPanel"));
        }
    }
}
