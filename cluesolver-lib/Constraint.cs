using System;
using System.Collections.Generic;

namespace cluesolver
{
    /// <summary>
    /// Represents a set of candidates, a known number of which must be owned by a particular owner
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Constraint<TOwner, TItem> where TItem : IEquatable<TItem>, IComparable<TItem>
    {
        public Constraint(TOwner owner, IEnumerable<TItem> candidates, int count = 1)
        {
            Owner = owner;
            CurrentCandidates = new SortedSet<TItem>(candidates);
            MinimumCandidates = count;
        }

        public TOwner Owner { get; }

        /// <summary>
        /// Raised when the constraint is solved (the number of candidates is reduced to the minimum)
        /// </summary>
        public event EventHandler<ConstraintSolvedEventArgs<TOwner>> Solved;

        /// <summary>
        /// Called when the constraint is solved
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSolved(ConstraintSolvedEventArgs<TOwner> e) => Solved?.Invoke(this, e);

        /// <summary>
        /// A set of items that includes the candidates that must be owned by a particular owner
        /// </summary>
        /// <value></value>
        private ISet<TItem> CurrentCandidates { get; }

        public ISet<TItem> Candidates => new SortedSet<TItem>(CurrentCandidates);

        /// <summary>
        /// The number of candidates that must be owned by a particular owner
        /// </summary>
        /// <value></value>
        public int MinimumCandidates { get; }

        public void RemoveCandidate(TItem candidate)
        {
            if (!IsSolved)
            {
                CurrentCandidates.Remove(candidate);
                if (IsSolved)
                {
                    OnSolved(new ConstraintSolvedEventArgs<TOwner>(Owner));
                }
            }
        }

        public bool IsSolved => CurrentCandidates.Count == MinimumCandidates;
    }

    public class ConstraintSolvedEventArgs<T> : EventArgs
    {
        public ConstraintSolvedEventArgs(T owner)
        {
            Owner = owner;
        }

        public T Owner { get; }
    }
}