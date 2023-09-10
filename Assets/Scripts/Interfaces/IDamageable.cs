interface IDamageable
{
    float MaxHP{get;set;}
    float CurrentHP{get;set;}
    bool IsAlive {get;set;}
    bool IsInvincible{get;set;}
    
    void OnHit(AttackAction source)
    {
        if(IsAlive && IsInvincible)
        {
            CurrentHP-=source.AttackValue;
        }
    }
    
}