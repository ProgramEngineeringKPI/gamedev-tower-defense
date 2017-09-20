#pragma warning disable 1591
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Local
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable InconsistentNaming
namespace JetBrains.Annotations
{
    using System;

    /// <summary>
    /// This attribute is intended to mark publicly available API
    /// which should not be removed and so is treated as used
    /// </summary>
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    public sealed class EditorAssignedAttribute : Attribute
    {
        public EditorAssignedAttribute()
        {
        }
        
         

        public EditorAssignedAttribute([NotNull] string comment)
        {
            this.Comment = comment;
        }

        [NotNull]
        public string Comment { get; private set; }
    }

    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    public sealed class JsonDeserializedAttribute : Attribute
    {
        public JsonDeserializedAttribute()
        {
        }
        
        public JsonDeserializedAttribute([NotNull] string comment)
        {
            this.Comment = comment;
        }

        [NotNull]
        public string Comment { get; private set; }
    }
}