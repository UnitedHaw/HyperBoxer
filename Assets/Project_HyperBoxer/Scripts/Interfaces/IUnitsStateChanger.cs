using Assets.Scripts.Enums;
using System;

public interface IUnitsStateChanger: IUnitStateEvent
{
    public void SetState(UnitStates state);
}

public interface IUnitStateEvent
{
    UnitStates CurrentState { get; }
    
    event Action<UnitStates> OnStateChanged;
}