using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using HealthyHabits.Auth;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RapidApiAuthenticationExtensions
    {

        public static AuthenticationBuilder AddRapidApiAuthentication(
            this AuthenticationBuilder builder)
        {
            return builder.AddScheme<AuthenticationSchemeOptions, RapidApiAuthenticationHandler>("RapidApi", "RapidApi",
                (Action<AuthenticationSchemeOptions>) (delegate{}));
        }
    }
}