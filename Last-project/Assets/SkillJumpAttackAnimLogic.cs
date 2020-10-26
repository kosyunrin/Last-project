using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillJumpAttackAnimLogic : StateMachineBehaviour
{
    Transform Player = null;
    SkillLogic logic = null;
    public enum LogicNum
    {
       FirstLogic,
       SecondLogic,
       SkillStart,
       SKillEnd
    }
    public LogicNum LogicStage;
    private float ExitLastAnimTime = 0.0f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Player = IntancePlayerObject.intance.Player;
        logic = IntancePlayerObject.intance.skillLogic;
        ExitLastAnimTime = 0;
        if (LogicStage == LogicNum.SkillStart)
        {
            IntancePlayerObject.intance.vmeetinput.SetAnyStataFalseForJumpAttack(true);
        }
       
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (LogicStage == LogicNum.FirstLogic)
        {
            if (Player.position.y < logic.JumpAttckHeight)
            {
                animator.CrossFade(AnimChipName.HashAnimStage2Name, logic.JumpLastAttackAnimTime);
            }
        }
        else if (LogicStage == LogicNum.SecondLogic)
        {
           ExitLastAnimTime += Time.deltaTime;
            if(ExitLastAnimTime> logic.ExitLastJumpAttackTimer)
            {
                animator.CrossFade(AnimChipName.HashAnimStage3Name, 0.25f);
            }
        }
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
         if (LogicStage == LogicNum.SKillEnd)
        {
            IntancePlayerObject.intance.vmeetinput.SetAnyStataFalseForJumpAttack(false);
        }
    }
}
