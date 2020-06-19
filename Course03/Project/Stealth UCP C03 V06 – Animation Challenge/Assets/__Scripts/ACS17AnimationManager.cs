using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ACS17AnimationManager : MonoBehaviour
{
    // Read public mode and turnDir information from EnemyNav on parent GameObject
    // Try using anim.CrossFade("ACS_Walk", 0);

    private Animator _anim;
    private EnemyNav enemyNav;

    private EnemyNav.eMode mode;


    private void Start()
    {
        _anim = this.GetComponent<Animator>();
        enemyNav = this.transform.parent.GetComponent<EnemyNav>();

        ChangeAnimation();


        mode = enemyNav._mode;
    }

    float fadingTime = 0.01f;
    

    private void Update()
    {

        //If enemy's state isn't the same as last frame
        if (mode != enemyNav._mode)
        {
            
            ChangeAnimation();
            mode = enemyNav._mode;
        }

        Rotation();
        
    }


    void Rotation()
    {
        if (mode == EnemyNav.eMode.preMoveRot || mode == EnemyNav.eMode.postMoveRot)
        {
            if (enemyNav.turnDir == -1)
            {
                _anim.CrossFade("ACS_TurnLeft", 0);
            }
            if (enemyNav.turnDir == 1)
            {
                _anim.CrossFade("ACS_TurnRight", 0);
            }

        }
    }

    private void ChangeAnimation()
    {
        float fading = 0.01f;

        switch (enemyNav._mode)
        {

            case EnemyNav.eMode.move:
                _anim.CrossFade("ACS_Walk", fading);
                break;
            case EnemyNav.eMode.wait:
                _anim.CrossFade("ACS_Idle", fading);
                break;
        }
    }



}
