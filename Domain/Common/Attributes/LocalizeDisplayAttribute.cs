using System.ComponentModel.DataAnnotations;

namespace Domain.Common.Attributes;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class LocalizeDisplayAttribute : Attribute
    {
        private readonly DisplayAttribute _inner;

        public LocalizeDisplayAttribute(string name)
        {
            _inner = new DisplayAttribute
            {
                Name = name,
                ResourceType = typeof(Domain.Localization.Resources)
            };
        }

        public string? GetName() => _inner.GetName();
    }