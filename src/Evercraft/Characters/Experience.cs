using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Evercraft.Characters
{
    public class Experience : ReactiveObject
    {
        private int _incrementalExperience;

        public Experience()
        {
            _increment
                .Scan(Total, (total, gained) => total + gained)
                .ToPropertyEx(this, experience => experience.Total);

            this.WhenAnyValue(x => x.Total)
                .Select(experience =>
                {
                    if (experience < 1000)
                    {
                        return experience;
                    }

                    Math.DivRem(experience, 1000, out var remainder);
                    return remainder;
                })
                .ToPropertyEx(this, experience => experience.Current);
        }

        /// <summary>
        /// Total accumulated experience.
        /// </summary>
        public int Total { [ObservableAsProperty] get; }

        /// <summary>
        /// Experience until next level.
        /// </summary>
        public int Current { [ObservableAsProperty] get; }

        public void Increase(int experience) => _increment.OnNext(experience);

        private ISubject<int> _increment = new Subject<int>();
    }
}