﻿using System;
using NuGet.Packaging.Core;

namespace OmniSharp.MSBuild.ProjectFile
{
    public class PackageReference : IEquatable<PackageReference>
    {
        public PackageIdentity Identity { get; }
        public bool IsImplicitlyDefined { get; }

        public PackageReference(PackageIdentity identity, bool isImplicitlyDefined)
        {
            this.Identity = identity;
            this.IsImplicitlyDefined = isImplicitlyDefined;
        }

        public override string ToString()
        {
            var version = Identity.HasVersion ? ", " + Identity.Version.ToNormalizedString() : string.Empty;
            var implicitSuffix = IsImplicitlyDefined ? " (implicit)" : string.Empty;

            return Identity.Id + version + implicitSuffix;
        }

        public bool Equals(PackageReference other)
        {
            if (!Identity.Equals(other.Identity))
            {
                return false;
            }

            return IsImplicitlyDefined == other.IsImplicitlyDefined;
        }

        public override int GetHashCode()
        {
            return this.Identity.GetHashCode() + (IsImplicitlyDefined ? 1 : 0);
        }
    }
}
