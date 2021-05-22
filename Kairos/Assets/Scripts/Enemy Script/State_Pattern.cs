using System;
using UnityEngine;

 interface State_Pattern
{ 
    void Alive();
    void Dead();
}

  class StateMachine
{
    public State_Pattern currentState;

     public void ChangeState(State_Pattern newState, int lifePoints)
    {
        if (lifePoints < 1)
        {
            currentState = newState;
            currentState.Dead();
        }
        else if (lifePoints > 1) {
            currentState = newState;
            currentState.Alive();
        }
       
    }

   
}

 public class TestState : State_Pattern
{
    MonoBehaviour owner;

    public TestState(MonoBehaviour owner) { this.owner = owner; }

     public void Alive()
    {
        Debug.Log("GameObject is alive");
    }

   public void Dead()
    {
        Debug.Log("GameObject is dead");
    }

}
