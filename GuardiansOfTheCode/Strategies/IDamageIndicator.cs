using System;
namespace GuardiansOfTheCode.Strategies
{
    public interface IDamageIndicator
    {
        void NotifyaboutDamage(int health, int damage);
    }
}

