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
            CandidateSet = new SortedSet<TItem>(candidates);
            NumberOfOwnedCandidates = count;
        }

        /// <summary>
        /// The owner that must own at least <see cref="Constraint.NumberOfOwnedCandidates"/> number of the items in <see cref="Constraint.Candidates"/>
        /// </summary>
        /// <value></value>
        public TOwner Owner { get; }

        /// <summary>
        /// Raised when the constraint is solved (the number of candidates is reduced to <see cref="Constraint.NumberOfOwnedCandidates"/>)
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
        private ISet<TItem> CandidateSet { get; }

        public IEnumerable<TItem> Candidates => CandidateSet;

        /// <summary>
        /// The number of candidates that must be owned by a particular owner
        /// </summary>
        /// <value></value>
        public int NumberOfOwnedCandidates { get; }

        public void RemoveCandidate(TItem candidate)
        {
            if (!IsSolved)
            {
                CandidateSet.Remove(candidate);
                if (IsSolved)
                {
                    OnSolved(new ConstraintSolvedEventArgs<TOwner>(Owner));
                }
            }
        }

        public bool IsSolved => CandidateSet.Count == NumberOfOwnedCandidates;
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