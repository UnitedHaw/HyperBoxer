using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Project_HyperBoxer.Scripts.UI
{
    public class WindowView
    {
        protected UIDocument _rootDocument;
        public WindowView(UIDocument document)
        {
            _rootDocument = document;
        }
    }
}
