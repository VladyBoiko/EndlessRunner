namespace Player
{
    public enum PlayerControllerState : byte
    {
        None = 0,
        Idle = 1,
        Movement = 2,
        Jump = 3,
        Fall = 4,
        Slide = 5,
        LedgeClimb = 6,
        LedgeHang = 7,
        
        NullState = 255
    }
}