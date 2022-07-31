using System;
using System.Collections.Generic;
using System.Text;
using Common;
using GuardiansOfTheCode.Events;

namespace GuardiansOfTheCode
{
    public sealed class PrimaryPlayer
    {
        private static readonly PrimaryPlayer _instance;

        private int _health;

        private PrimaryPlayer() { }

        public IWeapon Weapon { get; set; }

        public Card[] Cards { get; set; }

        private event EventHandler<HealthChangedEventArgs> HealthChanged;

        public void RegisterObserver(EventHandler<HealthChangedEventArgs> observer)
        {
            HealthChanged += observer;
        }

        public void UnRegisterObserver(EventHandler<HealthChangedEventArgs> observer)
        {
            HealthChanged -= observer;
        }

        static PrimaryPlayer()
        {
            _instance = new PrimaryPlayer()
            {
                Name = "Raptor",
                Level = 1,
                Armor = 25,
                Health = 100
            };
        }

        public static PrimaryPlayer Instance
        {
            get
            {
                return _instance;
            }
        }

        public string Name { get; set; }
        public int Level { get; set; }
        public int Armor { get; set; }
        public int Health
        {
            get
            {
                return _health;
            }
            private set
            {
                _health = value;
                HealthChanged?.Invoke(this, new HealthChangedEventArgs(value));
            }
        }

        public void Hit(int damage)
        {
            Health -= damage;
        }
    }
}
