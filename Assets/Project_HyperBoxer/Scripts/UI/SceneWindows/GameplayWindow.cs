using Assets.Project_HyperBoxer.Scripts.Interfaces.UI;
using Assets.Project_HyperBoxer.Scripts.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Project_HyperBoxer.Scripts.UI
{
    public class GameplayWindow : SceneWindow
    {
        private CombatControlWindow _combatControl;

        public GameplayWindow(UIDocument rootDocument) : base(rootDocument)
        {
            _combatControl = new CombatControlWindow(_rootDocument);
            _uiWindows.Add(_combatControl);
        }
    }
}
