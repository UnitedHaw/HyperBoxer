using Assets.Project_HyperBoxer.Scripts.Combat;
using System;

namespace Assets.Project_HyperBoxer.Scripts
{
    public interface IControllerInitilizer : IDisposable
    {
        public void Setup(BoxerBase boxerbase);
    }
}
