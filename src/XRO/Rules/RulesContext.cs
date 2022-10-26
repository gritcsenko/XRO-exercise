using System.Diagnostics.CodeAnalysis;
using XRO.Domain;

namespace XRO.Rules;

public class RulesContext : IRulesContext
{
    private readonly IReadOnlyCollection<IRule> _rules;
    private readonly List<Func<FactsSet, FactsSet>> _actions = new();
    private bool _isHalted;

    public RulesContext(params IRule[] rules) => _rules = rules;

    public bool IsHalted => _isHalted;

    public void Add(IFact fact) => AddAll(new[] { fact });

    public void AddAll(IEnumerable<IFact> set)
    {
        var buffer = set.ToArray();
        _actions.Add(facts => new(facts.Facts.Concat(buffer)));
    }

    public void Remove(IFact fact) => RemoveAll(new[] { fact });

    public void RemoveAll(IEnumerable<IFact> set)
    {
        var buffer = set.ToArray();
        _actions.Add(facts => new(facts.Facts.Except(buffer, FactRemoveEqualityComparer.Instance)));
    }

    public IReadOnlyFactsSet Execute()
    {
        var set = new FactsSet(Enumerable.Empty<IFact>());

        var actions = _actions.ToArray();
        _actions.Clear();
        _isHalted = false;
        foreach (var action in actions)
        {
            set = action(set);
            foreach (var rule in _rules)
            {
                var result = rule.Matches(set);
                if (result.IsMatched)
                {
                    rule.Execute(this, result);

                    foreach (var a in _actions)
                    {
                        set = a(set);
                    }
                    _actions.Clear();

                    if (IsHalted)
                    {
                        return set;
                    }
                }
            }
        }

        return set;
    }

    public void Halt()
    {
        _isHalted = true;
    }

    private class FactRemoveEqualityComparer : IEqualityComparer<IFact>
    {
        public static IEqualityComparer<IFact> Instance { get; } = new FactRemoveEqualityComparer();

        public bool Equals(IFact? x, IFact? y) => ReferenceEquals(x, y);

        public int GetHashCode([DisallowNull] IFact obj) => obj.GetHashCode();
    }
}
