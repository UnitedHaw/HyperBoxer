using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Project_HyperBoxer.Scripts.UI
{
    public class CombatControlWindow : UIWindow
    {
        public event Action LeftHandPunch;
        public event Action LeftLegPunch;
        public event Action RightHandPunch;
        public event Action RightLegPunch;

        private CombatControlView _combatControlView;
        public CombatControlWindow(UIDocument document) : base(document)
        {
            _combatControlView = new CombatControlView(document);
            _view = _combatControlView;
            SetupButtons();
        }

        protected override void Purify()
        {
            _combatControlView.LeftPanel.Hand.clicked -= LeftHandPunch;
            _combatControlView.LeftPanel.Hand.clicked -= LeftLegPunch;
            _combatControlView.RightPanel.Hand.clicked -= RightHandPunch;
            _combatControlView.RightPanel.Hand.clicked -= RightLegPunch;
        }

        private void SetupButtons()
        {
            _combatControlView.LeftPanel.Hand.clicked += LeftHandPunch;
            _combatControlView.LeftPanel.Hand.clicked += LeftLegPunch;
            _combatControlView.RightPanel.Hand.clicked += RightHandPunch;
            _combatControlView.RightPanel.Hand.clicked += RightLegPunch;
        }
    }
}
