using Assets.Project_HyperBoxer.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project_HyperBoxer.Scripts.Interfaces.UI
{
    public interface ISceneWindow : IDisposable
    {
        public List<UIWindow> UIWindows { get; }
    }
}
