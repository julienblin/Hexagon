// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetCurrentCultureInContextInterceptor.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Gets the <see cref="CultureInfo.CurrentCulture"/> and <see cref="CultureInfo.CurrentCulture"/>
//   <see cref="CultureInfo.Name"/> and puts it into the <see cref="IMessage.Context"/> <see cref="IContext.Headers"/>
//   using the key <see cref="ContextHeaderKey"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Impl
{
    using System.Globalization;

    using Hexagon.Messages;

    /// <summary>
    /// Gets the <see cref="CultureInfo.CurrentCulture"/> and <see cref="CultureInfo.CurrentUICulture"/>
    /// <see cref="CultureInfo.Name"/> and puts it into the <see cref="IMessage.Context"/> <see cref="IContext.Headers"/>
    /// using the <see cref="CultureHeaderKey"/> and <see cref="UICultureHeaderKey"/> keys.
    /// </summary>
    /// <remarks>
    /// See <see cref="SetCurrentCultureFromContextInterceptor"/> for the reverse operation.
    /// </remarks>
    public class SetCurrentCultureInContextInterceptor : IRequestProcessorInterceptor
    {
        /// <summary>
        /// The <see cref="IContext.Headers"/> key used for <see cref="CultureInfo.CurrentCulture"/>.
        /// </summary>
        public const string CultureHeaderKey = @"Culture";

        /// <summary>
        /// The <see cref="IContext.Headers"/> key used for <see cref="CultureInfo.CurrentUICulture"/>.
        /// </summary>
        public const string UICultureHeaderKey = @"UICulture";

        /// <summary>
        /// Gets the interception priority.
        /// Defined as <see cref="DefaultInterceptionPrority.Low"/>.
        /// </summary>
        public int InterceptionPriority
        {
            get
            {
                return DefaultInterceptionPrority.Low;
            }
        }

        /// <inheritdoc />
        public void Intercept(IRequestProcessorInvocation invocation)
        {
            invocation.Request.Context.Headers[CultureHeaderKey] = CultureInfo.CurrentCulture.Name;
            invocation.Request.Context.Headers[UICultureHeaderKey] = CultureInfo.CurrentUICulture.Name;
            invocation.Proceed();
        }
    }
}
