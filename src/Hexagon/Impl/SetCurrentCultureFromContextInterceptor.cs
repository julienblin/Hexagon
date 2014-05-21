// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetCurrentCultureFromContextInterceptor.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Sets the <see cref="Thread.CurrentCulture" /> and <see cref="Thread.CurrentUICulture" /> properties
//   using the values stored in <see cref="SetCurrentCultureInContextInterceptor.CultureHeaderKey" />
//   and <see cref="SetCurrentCultureInContextInterceptor.UICultureHeaderKey" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Impl
{
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// Sets the <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/> properties
    /// using the values stored in <see cref="SetCurrentCultureInContextInterceptor.CultureHeaderKey"/>
    /// and <see cref="SetCurrentCultureInContextInterceptor.UICultureHeaderKey"/>, if any.
    /// </summary>
    /// <remarks>
    /// See <see cref="SetCurrentCultureInContextInterceptor"/> for the reverse operation.
    /// </remarks>
    public class SetCurrentCultureFromContextInterceptor: IRequestProcessorInterceptor
    {
        /// <summary>
        /// Gets the interception priority.
        /// Defined as <see cref="DefaultInterceptionPrority.High"/>.
        /// </summary>
        public int InterceptionPriority
        {
            get
            {
                return DefaultInterceptionPrority.High;
            }
        }

        /// <inheritdoc />
        public void Intercept(IRequestProcessorInvocation invocation)
        {
            if (invocation.Request.Context.Headers.ContainsKey(SetCurrentCultureInContextInterceptor.CultureHeaderKey))
            {
                Thread.CurrentThread.CurrentCulture =
                    CultureInfo.GetCultureInfo(
                        invocation.Request.Context.Headers[SetCurrentCultureInContextInterceptor.CultureHeaderKey]);
            }

            if (invocation.Request.Context.Headers.ContainsKey(SetCurrentCultureInContextInterceptor.UICultureHeaderKey))
            {
                Thread.CurrentThread.CurrentUICulture =
                    CultureInfo.GetCultureInfo(
                        invocation.Request.Context.Headers[SetCurrentCultureInContextInterceptor.UICultureHeaderKey]);
            }

            invocation.Proceed();
        }
    }
}
