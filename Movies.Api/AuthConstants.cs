using System;

namespace Movies.Api;

public class AuthConstants
{
    public const string AdminUserPolicyName = "Admin";
    public const string AdminUserClaimName = "admin";
    public const string TrustedMemberPolicyName = "Trusted";
    public const string TrustedMemberClaimName = "trusted_member";
}
