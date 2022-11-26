using UnityEngine;

public struct PlayerInput
{
    public Vector2 mDelta;
    public Vector3 keyState;
    public bool m1State;
    public bool reload;
    public void tick()
    {
        mDelta.x = Input.GetAxisRaw("MouseX");
        mDelta.y = Input.GetAxisRaw("MouseY");

        keyState.x = Input.GetAxisRaw("MoveX");
        keyState.y = Input.GetAxisRaw("MoveY");
        keyState.z = Input.GetAxisRaw("MoveZ");




        if(Input.GetAxisRaw("Fire1") != 0)
            m1State = true;
        else
            m1State = false;



        if(Input.GetAxisRaw("Reload") != 0)
            reload = true;
    }
}
