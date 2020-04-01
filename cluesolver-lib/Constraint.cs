using System;
using System.Collections.Generic;

namespace cluesolver
{
    /// <summary>
    /// Represents a set of candidates, a known number of which must be owned by a particular owner
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Constraint<T> where T : IEquatable<T>
    {
        public Constraint(IEnumerable<T> collection, int count = 1)
        {
            Candidates = new HashSet<T>(collection);
            NumberOfCandidatesOwned = count;
        }

        /// <summary>
        /// A set of items that includes the candidates that must be owned by a particular owner
        /// </summary>
        /// <value></value>
        public ISet<T> Candidates { get; }

        /// <summary>
        /// The number of candidates that must be owned by a particular owner
        /// </summary>
        /// <value></value>
        public int NumberOfCandidatesOwned { get; }


        public bool IsSolved => Candidates.Count == NumberOfCandidatesOwned;
    }
}