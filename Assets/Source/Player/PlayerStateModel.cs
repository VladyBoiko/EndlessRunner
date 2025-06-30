namespace Player
{
    public class PlayerStateModel
    {
        public int JumpCount { get; private set; }
        public bool IsOnWall { get; private set; }
        public bool IsHanging { get; private set; }
        public bool IsClimbing { get; private set; }
        public float HangCooldownTimer { get; private set; }
        
        public void AddJumpCount()
        {
            JumpCount++;
        }
        
        public void SetJumpCount(int jumpCount)
        {
            JumpCount = jumpCount >= 0 ? jumpCount : 0;
        }
        
        public void SetIsOnWall(bool isOnWall)
        {
            IsOnWall = isOnWall;
        }
        
        public void SetIsHanging(bool isHanging)
        {
            IsHanging = isHanging;
        }
        
        public void SetIsClimbing(bool isClimbing)
        {
            IsClimbing = isClimbing;
        }
        
        public void SetHangCooldownTimer(float hangCooldownTimer)
        {
            HangCooldownTimer = hangCooldownTimer >= 0 ? hangCooldownTimer : 0;
        }
    }
}