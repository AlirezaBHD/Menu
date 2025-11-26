using System.ComponentModel.DataAnnotations;

namespace Muno.Domain.Common.Attributes;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LocalizeDisplayAttribute(string name) : Attribute
    {
        private readonly DisplayAttribute _inner = new()
        {
            Name = name,
            ResourceType = typeof(Localization.Resources)
        };

        public string? GetName() => _inner.GetName();
    }