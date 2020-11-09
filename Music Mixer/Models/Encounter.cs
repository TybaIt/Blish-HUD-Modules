﻿using Blish_HUD;
using Blish_HUD.ArcDps.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nekres.Music_Mixer
{
    internal class Encounter
    {
        /// <summary>
        /// The name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Relevant agent ids.
        /// </summary>
        public IReadOnlyList<uint> Ids { get; private set; }

        private readonly long _initialHealth;
        /// <summary>
        /// The crrent health.
        /// </summary>
        public long Health { get; private set; }
        /// <summary>
        /// Unique identifier.
        /// </summary>
        /// <remarks>
        /// Changes between entity (re-)spawn.
        /// </remarks>
        public ulong SessionId { get; private set; }

        private long _enrageTimer;
        private DateTime _startTime;
        /// <summary>
        /// If the encounter is enraged.
        /// </summary>
        /// <remarks>
        /// Ie. enrage timer has passed.
        /// </remarks>
        public bool Enraged { get => _enrageTimer < DateTime.Now.Subtract(_startTime).Milliseconds; }

        public IReadOnlyList<int> Phases { get; private set; }
        public IReadOnlyList<long> Times { get; private set; }

        public event EventHandler<ValueEventArgs<int>> PhaseChanged;

        private int _currentPhase;
        public int CurrentPhase { 
            get => _currentPhase;
            private set {
                if (_currentPhase == value) return;

                _currentPhase = value;

                PhaseChanged?.Invoke(this, new ValueEventArgs<int>(value));
            }
        }

        public Encounter(EncounterData data, ulong sessionId) {
            Name = data.Name;
            Ids = data.Ids;
            _initialHealth = data.Health;
            Health = data.Health;
            SessionId = sessionId;
            Times = data.Times;
            Phases = data.Phases;
            CurrentPhase = 0;
            _enrageTimer = data.EnrageTimer;
            _startTime = DateTime.Now;
        }
        /// <summary>
        /// Tries to decrement the current health given the damaging combat event.
        /// </summary>
        /// <param name="e">A combat event with damaging fields.</param>
        public void DoDamage(Ev e) {
            if (e.IsStateChange || e.IsActivation || e.IsBuffRemove) return;
            var damage = 0;
            if (e.Buff) {
                if (e.BuffDmg < 1) return;
                damage = e.BuffDmg;
            } else {
                damage = e.Value;
            }
            switch (e.Result) {
                case 3: return; // blocked
                case 4: return; // evaded
                case 6: return; // absorbed
                case 7: return; // missed
                default: Health -= damage; break;
            }
        }
        /// <summary>
        /// Gets the current health percentage.
        /// </summary>
        /// <returns></returns>
        public float GetHealthPercent() {
            return _initialHealth == 0 ? 0 : Health / _initialHealth;
        }
    }
}