using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Project_HyperBoxer.Scripts.UI
{
    public class PunchesPanel
    {
        private VisualElement _panelRoot;

        public Button Hand { get; }
        public Button Leg { get; }
        public PunchesPanel(VisualElement panelRoot)
        {
            _panelRoot = panelRoot;

            Hand = _panelRoot.Q<Button>("Hand");
            Leg = _panelRoot.Q<Button>("Leg");
        }
    }
}
